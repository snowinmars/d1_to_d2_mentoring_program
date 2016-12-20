using Bcl.Common.Resources;
using Bcl.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Bcl.Common;

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
                this.Dispose();
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
            this.logger.Write(BclResource.Started);
            this.fsv.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            this.logger.Write(BclResource.Stopped);
            this.fsv.EnableRaisingEvents = false;
        }

        private void Configurate()
        {
            this.config = WatcherConfig.Load();
            this.logger = WatcherLogger.Load();

            this.HandleConfig();
            this.ConfigurateLogger();

            this.ConfigurateFileSystemWatcher();

            this.logger.Write(this.config, this.logger);

            BclResource.Culture = this.config.CultureInfo;
        }

        private void ConfigurateFileSystemWatcher()
        {
            this.fsv.NotifyFilter = Watcher.AllNotifyFilters;
            this.fsv.Filter = Watcher.DefaultFilter;
            this.fsv.Path = this.config.SourceDirectories.First();

            this.fsv.Created += (sender, args) => this.HandleFileOrDirectory(args);
        }

        private void ConfigurateLogger()
        {
            this.logger.IsEnabled = this.config.IsVerbose;

            #region actionBinding

            this.fsv.Created += (sender, args) =>
            {
                if (this.config.IsVerbose)
                {
                    this.logger.Write(string.Format(BclResource.OnCreate, args.FullPath));
                }
            };
            this.fsv.Changed += (sender, args) =>
            {
                if (this.config.IsVerbose)
                {
                    this.logger.Write(string.Format(BclResource.OnChange, args.FullPath));
                }
            };
            this.fsv.Deleted += (sender, args) =>
            {
                if (this.config.IsVerbose)
                {
                    this.logger.Write(string.Format(BclResource.OnDelete, args.FullPath));
                }
            };
            this.fsv.Renamed += (sender, args) =>
            {
                if (this.config.IsVerbose)
                {
                    this.logger.Write(string.Format(BclResource.OnRename, args.OldFullPath, args.FullPath));
                }
            };
            this.fsv.Error += (sender, args) =>
            {
                if (this.config.IsVerbose)
                {
                    this.logger.Write(string.Format(BclResource.OnError, args.GetException().Message));
                }
            };

            #endregion actionBinding
        }

        private void HandleConfig()
        {
            this.config.SourceDirectories
                .Where(sourceDir => !Directory.Exists(sourceDir))
                .ForEach(Directory.CreateDirectory);

            this.config.WatcherRules
                .Select(rule => rule.DestinationFolder)
                .Where(destinationDir => !Directory.Exists(destinationDir))
                .ForEach(Directory.CreateDirectory);
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
                Thread.Sleep(30); // ms
            }

            string date = this.config.MustAddDate
                            ? DateTime.Now.ToString("yyyy MMM dd - hh mm ss", this.config.CultureInfo) // culture based?
                            : string.Empty;

            string num = this.config.MustAddNumber
                ? Directory.EnumerateFileSystemEntries(Path.GetDirectoryName(to),
                                                        Watcher.DefaultFilter,
                                                        SearchOption.TopDirectoryOnly)
                            .Count()
                            .ToString(this.config.CultureInfo)
                : string.Empty;
            
            string dir = Path.GetDirectoryName(to);
            string file = Path.GetFileName(to);

            StringBuilder sb = new StringBuilder(256);

            if (!string.IsNullOrWhiteSpace(num))
            {
                sb.Append(num);
                sb.Append(". ");
            }

            if (!string.IsNullOrWhiteSpace(date))
            {
                sb.Append(date);
                sb.Append(" - ");
            }

            sb.Append(file);

            string result = Path.Combine(dir, sb.ToString());

            File.Move(from, result);

            if (this.config.IsVerbose)
            {
                this.logger.Write(string.Format(BclResource.OnMove, from, result));
            }
        }
    }
}