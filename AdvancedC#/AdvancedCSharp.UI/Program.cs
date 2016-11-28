using System;
using AdvancedCSharp.Core;
using AdvancedCSharp.Extensions;

namespace AdvancedCSharp.UI
{
    internal class Program
    {
        private static void Main()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => 
                                                            info.Exists);

            fsv.OnStart += (sender, args) => { Console.WriteLine("Started"); };
            fsv.OnFinish += (sender, args) => { Console.WriteLine("Finished"); };

            fsv.OnFileFinded += (sender, args) => { Console.WriteLine($"Found file. Name: {args.Message}"); };
            fsv.OnFilteredFileFinded += (sender, args) => { Console.WriteLine($"Found filtered file. Name: {args.Message}"); };

            fsv.OnDirectoryFinded += (sender, args) => { Console.WriteLine($"Found dir. Name: {args.Message}"); };
            fsv.OnFilteredDirectoryFinded += (sender, args) => { Console.WriteLine($"Found filtered dir. Name: {args.Message}"); };

            var result = fsv.SearchByFilter(@"D:\prg\mentoring\mentoring\Advanced C#\implementation\AdvancedCSharp\AdvancedCSharp.Core", true)
                            .RepresentAsString();

        }
    }
}