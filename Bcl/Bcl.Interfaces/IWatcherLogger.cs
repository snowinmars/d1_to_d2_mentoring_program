namespace Bcl.Interfaces
{
    public interface IWatcherLogger
    {
        /// <summary>
        /// Enable writing to log functionality
        /// </summary>
        bool IsEnabled { get; set; }

        void Write(string str);

        void Write(object obj);

        void Write(params object[] objects);

        void Write(params string[] strings);
    }
}