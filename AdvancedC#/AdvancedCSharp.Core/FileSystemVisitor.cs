using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedCSharp.Core
{
    public class FileSystemVisitor
    {
        private enum FileSystemVisitorAction
        {
            None = 0,
            Ignore = 1,
            Interrupt = 2,
        }

        private const string DefaultSearchPattern = "*.*";
        public static readonly Func<FileSystemInfo, bool> DefaultFilter = info => true;
        private FileSystemVisitorAction action;

        public FileSystemVisitor() : this(FileSystemVisitor.DefaultFilter)
        {
        }

        public FileSystemVisitor(Func<FileSystemInfo, bool> filter)
        {
            this.Filter = filter;
            this.action = FileSystemVisitorAction.None;
        }

        public Func<FileSystemInfo, bool> Filter { get; set; }

        public IEnumerable<FileSystemInfo> SearchByFilter(string path, bool isRecursive = false)
        {
            this.OnStart?.Invoke(this, new EventArgs());

            IEnumerable<FileSystemInfo> result;

            if (Directory.Exists(path))
            {
                SearchOption searchOption = (isRecursive ? SearchOption.AllDirectories :
                                                            SearchOption.TopDirectoryOnly);

                DirectoryInfo info = new DirectoryInfo(path);

                IEnumerable<FileSystemInfo> entries = info.EnumerateFileSystemInfos(FileSystemVisitor.DefaultSearchPattern, searchOption);

                result = this.HandleEntries(entries).ToList();
            }
            else
            {
                result = new List<FileSystemInfo>();
            }

            this.OnFinish?.Invoke(this, new EventArgs());

            return result;
        }

        private IEnumerable<FileSystemInfo> HandleEntries(IEnumerable<FileSystemInfo> entries)
        {
            foreach (var entry in entries)
            {
                if (this.action.HasFlag(FileSystemVisitorAction.Interrupt))
                {
                    break;
                }

                if (this.action.HasFlag(FileSystemVisitorAction.Ignore)) // bug
                {
                    continue;
                }

                DirectoryInfo directoryInfo = entry as DirectoryInfo;
                FileInfo fileInfo = entry as FileInfo;
                bool isPassed = this.Filter(entry);

                if (directoryInfo != null)
                {
                    this.InvokeConsiderFilter(this.OnDirectoryFinded,
                            this,
                            new FileSystemVisitorEventArgs { Message = directoryInfo.FullName });

                    if (isPassed)
                    {
                        this.InvokeConsiderFilter(this.OnFilteredDirectoryFinded,
                            this,
                            new FileSystemVisitorEventArgs { Message = directoryInfo.FullName });

                        yield return entry;
                    }
                }

                if (fileInfo != null)
                {
                    this.InvokeConsiderFilter(this.OnFileFinded,
                            this,
                            new FileSystemVisitorEventArgs { Message = fileInfo.FullName });

                    if (isPassed)
                    {
                        this.InvokeConsiderFilter(this.OnFilteredFileFinded,
                            this,
                            new FileSystemVisitorEventArgs { Message = fileInfo.FullName });

                        yield return entry;
                    }
                }
            }
        }

        private void InvokeConsiderFilter(FileSystemVisitorEvent ev, object obj, FileSystemVisitorEventArgs args)
        {
            if (this.Filter != FileSystemVisitor.DefaultFilter)
            {
                ev?.Invoke(obj, args);
                this.ApplyEventArgsChanges(args);
            }
        }

        private void ApplyEventArgsChanges(FileSystemVisitorEventArgs args)
        {
            switch (args.State)
            {
                case FileSystemVisitorEventArgsStates.None:
                    break;
                case FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence:
                case FileSystemVisitorEventArgsStates.StopOnFirstFiltredFindedCoincidence:
                    this.action = FileSystemVisitorAction.Interrupt;
                    break;
                case FileSystemVisitorEventArgsStates.IgnoreThisEntry:
                    this.action = FileSystemVisitorAction.Ignore;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(args.State), args.State, "FileSystemVisitorEventArgs state is out of range");
            }
        }

        #region events

        public delegate void Event(object sender, EventArgs e);

        public delegate void FileSystemVisitorEvent(object sender, FileSystemVisitorEventArgs e);

        public event FileSystemVisitorEvent OnDirectoryFinded;

        public event FileSystemVisitorEvent OnFileFinded;

        public event FileSystemVisitorEvent OnFilteredDirectoryFinded;

        public event FileSystemVisitorEvent OnFilteredFileFinded;

        public event Event OnFinish;

        public event Event OnStart;

        #endregion events
    }
}