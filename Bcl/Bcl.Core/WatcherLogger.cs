using Bcl.Interfaces;
using System;
using System.Text;

namespace Bcl.Core
{
    public class WatcherLogger : IWatcherLogger
    {
        public bool IsEnabled { get; set; }

        public void Write(object obj)
        {
            this.Write(obj.ToString());
        }

        public void Write(params object[] objects)
        {
            foreach (var obj in objects)
            {
                this.Write(obj);
            }
        }

        public void Write(params string[] strings)
        {
            foreach (var str in strings)
            {
                this.Write(str);
            }
        }

        public void Write(string str)
        {
            if (this.IsEnabled)
            {
                Console.WriteLine(str);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.Append($"{nameof(WatcherLogger.IsEnabled)}: {this.IsEnabled}");
            sb.AppendLine();

            return sb.ToString();
        }

        public static IWatcherLogger Load()
        {
            IWatcherLogger logger = new WatcherLogger();

            return logger;
        }
    }
}