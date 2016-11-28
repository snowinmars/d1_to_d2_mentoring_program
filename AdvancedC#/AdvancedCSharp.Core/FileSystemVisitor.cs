using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdvancedCSharp.Core
{
    public class FileSystemVisitor
    {
        public static readonly Func<FileSystemInfo, bool> DefaultFilter = info => true;

        private static readonly object EmptyObject = new object();

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
                        FileSystemVisitor.EmptyObject,
                        new FileSystemVisitorEventArgs());

            if (!Directory.Exists(path))
            {
                this.OnFinish?.Invoke(FileSystemVisitor.EmptyObject, new FileSystemVisitorEventArgs());
                return new List<FileSystemInfo>();
            }

            var directories = this.GetDirectories(path, isRecursive);
            var files = this.GetFiles(path, isRecursive);

            this.InvokeConsiderFilter(this.OnFinish,
                        FileSystemVisitor.EmptyObject,
                        new FileSystemVisitorEventArgs());

            return directories.Concat(files);
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

        private IEnumerable<FileSystemInfo> GetDirectories(string path, bool isRecursive = false)
        {
            SearchOption searchOption = (isRecursive ? SearchOption.AllDirectories :
                                                        SearchOption.TopDirectoryOnly);

            var dirEnumerator = Directory.EnumerateDirectories(path, "*.*", searchOption).GetEnumerator();

            while (dirEnumerator.MoveNext())
            {
                string fullDirPath = dirEnumerator.Current;

                this.InvokeConsiderFilter(this.OnDirectoryFinded,
                            FileSystemVisitor.EmptyObject,
                            new FileSystemVisitorEventArgs { Message = fullDirPath });

                FileInfo dirInfo = new FileInfo(dirEnumerator.Current);

                if (this.Filter(dirInfo))
                {
                    this.InvokeConsiderFilter(this.OnFilteredDirectoryFinded,
                            FileSystemVisitor.EmptyObject,
                            new FileSystemVisitorEventArgs { Message = fullDirPath });

                    yield return dirInfo;
                }
            }
        }

        private IEnumerable<FileSystemInfo> GetFiles(string path, bool isRecursive = false)
        {
            SearchOption searchOption = (isRecursive ? SearchOption.AllDirectories :
                                                        SearchOption.TopDirectoryOnly);

            IEnumerator<string> fileEnumerator = Directory.EnumerateFiles(path, "*.*", searchOption).GetEnumerator();

            while (fileEnumerator.MoveNext())
            {
                string fullFilePath = fileEnumerator.Current;

                this.InvokeConsiderFilter(this.OnFileFinded,
                            FileSystemVisitor.EmptyObject,
                            new FileSystemVisitorEventArgs { Message = fullFilePath });

                FileInfo fileInfo = new FileInfo(fileEnumerator.Current);

                if (this.Filter(fileInfo))
                {
                    this.InvokeConsiderFilter(this.OnFilteredFileFinded,
                                FileSystemVisitor.EmptyObject,
                                new FileSystemVisitorEventArgs { Message = fullFilePath });

                    yield return fileInfo;
                }
            }
        }
    }
}