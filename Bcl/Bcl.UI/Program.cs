using Bcl.Core;
using Bcl.Interfaces;
using System;

namespace Bcl.UI
{
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