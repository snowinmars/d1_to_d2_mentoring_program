using System;
using System.Linq;

namespace ExceptionHandling.Task1.UI
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                string input = Console.ReadLine();
                Console.WriteLine(Core.IEnumerableExtension.Go(new[] { input}).First());
            }
        }
    }
}