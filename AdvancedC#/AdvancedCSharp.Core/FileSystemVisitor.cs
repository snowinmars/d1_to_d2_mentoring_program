using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedCSharp.Core
{
    public class FileSystemVisitor
    {
        private enum FileSystemVisitorRuntimeState
        {
            None = 0,
            Interrupt = 1,
            FoundFile = 2,
            FoundFiltredFile = 4,
            FoundDirectory = 8,
            FoundFiltredDirectory = 16,
        }

        private const string DefaultSearchPattern = "*.*";
        public static readonly Func<FileSystemInfo, bool> DefaultFilter = info => true;
        private FileSystemVisitorRuntimeState runtimeState;

        public FileSystemVisitor() : this(FileSystemVisitor.DefaultFilter)
        {
        }

        public FileSystemVisitor(Func<FileSystemInfo, bool> filter)
        {
            this.Filter = filter;
            this.runtimeState = FileSystemVisitorRuntimeState.None;
        }

        public Func<FileSystemInfo, bool> Filter { get; set; }

        public IEnumerable<FileSystemInfo> SearchByFilter(string path, bool isRecursive = false)
        {
            this.runtimeState = FileSystemVisitorRuntimeState.None;

            this.InvokeConsiderFilter(this.OnStart,
                        this,
                        new FileSystemVisitorEventArgs());

            IEnumerable<FileSystemInfo> result;

            if (Directory.Exists(path))
            {
                SearchOption searchOption = (isRecursive ? SearchOption.AllDirectories :
                                                            SearchOption.TopDirectoryOnly);

                DirectoryInfo info = new DirectoryInfo(path);

                IEnumerable<FileSystemInfo> directories = info.EnumerateDirectories(DefaultSearchPattern, searchOption);
                IEnumerable<FileSystemInfo> files = info.EnumerateFiles(DefaultSearchPattern, searchOption);

                result = this.HandleDirectories(directories).Concat(this.HandleFiles(files));
            }
            else
            {
                result = new List<FileSystemInfo>();
            }

            this.runtimeState = FileSystemVisitorRuntimeState.None;

            this.InvokeConsiderFilter(this.OnFinish,
                        this,
                        new FileSystemVisitorEventArgs());

            return result;
        }

        private IEnumerable<FileSystemInfo> HandleFiles(IEnumerable<FileSystemInfo> files)
        {
            foreach (var file in files.Select(fileSystemInfo => fileSystemInfo as FileInfo))
            {
                if (this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.Interrupt))
                {
                    break;
                }

                this.runtimeState = FileSystemVisitorRuntimeState.FoundFile;

                this.InvokeConsiderFilter(this.OnFileFinded,
                        this,
                        new FileSystemVisitorEventArgs { Message = file.FullName });

                bool isPassed = this.Filter(file);

                if (isPassed)
                {
                    this.runtimeState = FileSystemVisitorRuntimeState.FoundFiltredFile;

                    this.InvokeConsiderFilter(this.OnFilteredFileFinded,
                        this,
                        new FileSystemVisitorEventArgs { Message = file.FullName });

                    yield return file;
                }
            }
        }

        private IEnumerable<FileSystemInfo> HandleDirectories(IEnumerable<FileSystemInfo> directories)
        {
            foreach (var directory in directories.Select(fileSystemInfo => fileSystemInfo as DirectoryInfo))
            {
                if (this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.Interrupt))
                {
                    break;
                }

                this.runtimeState = FileSystemVisitorRuntimeState.FoundDirectory;

                this.InvokeConsiderFilter(this.OnDirectoryFinded,
                        this,
                        new FileSystemVisitorEventArgs { Message = directory.FullName });

                bool isPassed = this.Filter(directory);

                if (isPassed)
                {
                    this.runtimeState = FileSystemVisitorRuntimeState.FoundFiltredDirectory;

                    this.InvokeConsiderFilter(this.OnFilteredDirectoryFinded,
                        this,
                        new FileSystemVisitorEventArgs { Message = directory.FullName });

                    yield return directory;
                }
            }
        }

        private void InvokeConsiderFilter(MyEvent ev, object obj, FileSystemVisitorEventArgs args)
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
                    if (this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.FoundFile) ||
                        this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.FoundDirectory))
                    {
                        this.runtimeState = FileSystemVisitorRuntimeState.Interrupt;
                    }
                    break;
                case FileSystemVisitorEventArgsStates.StopOnFirstFiltredFindedCoincidence:
                    if (this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.FoundFiltredFile) ||
                        this.runtimeState.HasFlag(FileSystemVisitorRuntimeState.FoundFiltredDirectory))
                    {
                        this.runtimeState = FileSystemVisitorRuntimeState.Interrupt;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(args.State), args.State, "FileSystemVisitorEventArgs state is out of range");
            }
        }

        #region events

        public delegate void MyEvent(object sender, FileSystemVisitorEventArgs e);

        public event MyEvent OnDirectoryFinded;

        public event MyEvent OnFileFinded;

        public event MyEvent OnFilteredDirectoryFinded;

        public event MyEvent OnFilteredFileFinded;

        public event MyEvent OnFinish;

        public event MyEvent OnStart;

        #endregion events
    }
}