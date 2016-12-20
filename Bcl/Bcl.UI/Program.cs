using Bcl.Core;
using Bcl.Interfaces;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Bcl.UI
{
    [Flags]
    public enum A
    {
        a = 0,
        b = 1,
        c = 2,
        d = 4,
    }

    internal class Program
    {
        private static void Main()
        {
            using (IWatcher w = new Watcher())
            {
                w.Start();

                var c = Console.ReadLine();

                w.Stop();
            }
        }
    }
}