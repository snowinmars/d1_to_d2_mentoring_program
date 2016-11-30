using System;
using System.IO;

namespace AdvancedCSharp.Core
{
    [Flags]
    public enum FileSystemVisitorEventArgsStates
    {
        None = 0,
        StopOnFirstFindedCoincidence = 1,
        IgnoreThisEntry = 4,
    }

    public class FileSystemVisitorEventArgs : EventArgs
    {
        public string Message { get; set; }
        public FileSystemVisitorEventArgsStates State { get; set; }
        public FileSystemInfo Value { get; set; }
    }
}