using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedCSharp.Core
{
    public class FileSystemVisitor
    {
        private const string DefaultSearchPattern = "*.*";
        public static readonly Func<FileSystemInfo, bool> DefaultFilter = info => true;

        public FileSystemVisitor() : this(FileSystemVisitor.DefaultFilter)
        {
        }

        public FileSystemVisitor(Func<FileSystemInfo, bool> filter)
        {
            this.Filter = filter;
        }

        public Func<FileSystemInfo, bool> Filter { get; set; }

        public IEnumerable<FileSystemInfo> SearchByFilter(string path, bool isRecursive = false)
        {
            this.InvokeConsiderFilter(this.OnStart,
                        this,
                        new FileSystemVisitorEventArgs());

            if (!Directory.Exists(path))
            {
                this.InvokeConsiderFilter(this.OnFinish,
                        this,
                        new FileSystemVisitorEventArgs()); // TODO
                return new List<FileSystemInfo>();
            }

            SearchOption searchOption = (isRecursive ? SearchOption.AllDirectories :
                                                        SearchOption.TopDirectoryOnly);

            DirectoryInfo info = new DirectoryInfo(path);

            IEnumerable<FileSystemInfo> entries = info.EnumerateFileSystemInfos(DefaultSearchPattern, searchOption);

            IEnumerable<FileSystemInfo> result = this.HandleEntries(entries);

            this.InvokeConsiderFilter(this.OnFinish,
                        this,
                        new FileSystemVisitorEventArgs());

            return result;
        }

        private IEnumerable<FileSystemInfo> HandleEntries(IEnumerable<FileSystemInfo> entries)
        {
            foreach (var entry in entries)
            {
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

        private void InvokeConsiderFilter(MyEvent ev, object obj, FileSystemVisitorEventArgs args)
        {
            if (this.Filter != FileSystemVisitor.DefaultFilter)
            {
                ev?.Invoke(obj, args);
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