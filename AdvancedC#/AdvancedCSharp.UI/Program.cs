using AdvancedCSharp.Core;
using AdvancedCSharp.Extensions;
using System;
using System.IO;

namespace AdvancedCSharp.UI
{
    internal class Program
    {
        private static void Main()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info =>
                                                            info.Exists);

            Console.WriteLine("This is from events:");

            fsv.OnStart += (sender, args) => { Console.WriteLine("Started"); };
            fsv.OnFinish += (sender, args) => { Console.WriteLine("Finished"); };

            fsv.OnFileFinded += (sender, args) =>
            {
                Console.WriteLine($"Found file. Name: {args.Message}");
                if (args.Value.Name.Contains("C"))
                {
                    args.State = FileSystemVisitorEventArgsStates.IgnoreThisEntry;
                }
            };
            fsv.OnFilteredFileFinded += (sender, args) => { Console.WriteLine($"Found filtered file. Name: {args.Message}"); };

            fsv.OnDirectoryFinded += (sender, args) =>
            {
                Console.WriteLine($"Found dir. Name: {args.Message}");
                //args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
            };
            fsv.OnFilteredDirectoryFinded += (sender, args) => { Console.WriteLine($"Found filtered dir. Name: {args.Message}"); };

            var result = fsv.SearchByFilter(@"D:\testFolder", true);
                            


            Console.WriteLine("And this is the results:");

            foreach (var item in result)
            {
                Console.WriteLine(item.FullName);
            }
        }
    }
}