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
                                                            info.Name.Contains("C"));

            fsv.OnStart += (sender, args) => { Console.WriteLine("Started"); };
            fsv.OnFinish += (sender, args) => { Console.WriteLine("Finished"); };

            fsv.OnFileFinded += (sender, args) => {
                Console.WriteLine($"Found file. Name: {args.Message}");
                if (args.Message.Contains("12"))
                {
                    args.State = FileSystemVisitorEventArgsStates.StopOnFirstFiltredFindedCoincidence;
                }
            };
            fsv.OnFilteredFileFinded += (sender, args) => {
                Console.WriteLine($"Found filtered file. Name: {args.Message}");
                //args.State = FileSystemVisitorEventArgsStates.StopOnFirstFiltredFindedCoincidence;
            };

            fsv.OnDirectoryFinded += (sender, args) => {
                Console.WriteLine($"Found dir. Name: {args.Message}");
                //args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
            };
            fsv.OnFilteredDirectoryFinded += (sender, args) => { Console.WriteLine($"Found filtered dir. Name: {args.Message}"); };

            var result = fsv.SearchByFilter($"{Directory.GetCurrentDirectory()}\\..\\..", true)
                            .RepresentAsString();

            Console.ReadKey();
        }
    }
}