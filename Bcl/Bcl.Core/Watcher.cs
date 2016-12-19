using Bcl.Enums;
using Bcl.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bcl.Core
{
    public class Watcher : IWatcher, IDisposable
    {
        private const NotifyFilters AllNotifyFilters = NotifyFilters.Attributes |
                                          NotifyFilters.CreationTime |
                                          NotifyFilters.DirectoryName |
                                          NotifyFilters.FileName |
                                          NotifyFilters.LastAccess |
                                          NotifyFilters.LastWrite |
                                          NotifyFilters.Security |
                                          NotifyFilters.Size;

        private const string DefaultFilter = @"*.*";

        private readonly FileSystemWatcher fsv;

        private IWatcherConfig config;

        private IWatcherLogger logger;

        public Watcher()
        {
            try
            {
                this.fsv = new FileSystemWatcher();
            }
            catch (Exception)
            {
                this.fsv.Dispose();
                throw;
            }

            this.Configurate();
        }

        public void Dispose()
        {
            this.fsv.Dispose();
        }

        public void Start()
        {
            this.logger.Write("Started");
            this.fsv.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            this.logger.Write("Stopped");
            this.fsv.EnableRaisingEvents = false;
        }

        private void Configurate()
        {
            this.config = WatcherConfig.Load();
            this.logger = WatcherLogger.Load();

            this.ConfigurateLogger();

            this.ConfigurateFileSystemWatcher();

            this.logger.Write(this.config, this.logger);
        }

        private void ConfigurateFileSystemWatcher()
        {
            this.fsv.NotifyFilter = Watcher.AllNotifyFilters;
            this.fsv.Filter = Watcher.DefaultFilter;
            this.fsv.Path = this.config.DirectoriesToListenFor.First();

            this.fsv.Created += (sender, args) => this.HandleFileOrDirectory(args);
        }

        private void ConfigurateLogger()
        {
            this.logger.IsEnabled = this.config.IsVerbose;

            this.fsv.Created += (sender, args) => this.logger.Write($"Created {args.FullPath}");
            this.fsv.Changed += (sender, args) => this.logger.Write($"Changed {args.FullPath}");
            this.fsv.Deleted += (sender, args) => this.logger.Write($"Deleted {args.FullPath}");
            this.fsv.Renamed += (sender, args) => this.logger.Write($"Renamed from {args.OldFullPath} to {args.FullPath}");
            this.fsv.Error += (sender, args) => this.logger.Write($"Error: {args.GetException().Message}");
        }

        private void HandleFileOrDirectory(FileSystemEventArgs args)
        {
            string fileName = args.Name;
            string fileFullPath = args.FullPath;

            foreach (var watcherRule in this.config.WatcherRules)
            {
                if (Regex.IsMatch(fileName,
                                    watcherRule.Regex,
                                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase))
                {
                    string newFilePath = Path.Combine(watcherRule.DestinationFolder, fileName);

                    if (!File.Exists(newFilePath))
                    {
                        this.TryToMove(fileFullPath, newFilePath);
                    }

                    return;
                }
            }

            string defaultFilePath = Path.Combine(this.config.DefaultDestinationFolder, fileName);

            if (!File.Exists(defaultFilePath))
            {
                this.TryToMove(fileFullPath, defaultFilePath);
            }
        }

        private void TryToMove(string from, string to)
        {
            while (FileUtils.WhoIsLocking(from).Count != 0)
            {
                Thread.Sleep(50);
            }

            File.Move(from, to);
        }
    }
}