using System;

namespace AdvancedCSharp.Core
{
    public enum FileSystemVisitorEventArgsStates
    {
        None = 0,
        StopOnFirstCoincidence = 1,
    }

    public class FileSystemVisitorEventArgs : EventArgs
    {
        public FileSystemVisitorEventArgsStates State { get; set; }

        public string Message { get; set; }
    }
}