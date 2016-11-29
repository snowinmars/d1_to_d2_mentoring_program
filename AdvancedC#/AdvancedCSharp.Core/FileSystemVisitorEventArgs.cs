using System;

namespace AdvancedCSharp.Core
{
    [Flags]
    public enum FileSystemVisitorEventArgsStates
    {
        None = 0,
        StopOnFirstFindedCoincidence = 1,
        StopOnFirstFiltredFindedCoincidence = 2,
    }

    public class FileSystemVisitorEventArgs : EventArgs
    {
        public string Message { get; set; }
        public FileSystemVisitorEventArgsStates State { get; set; }
    }
}