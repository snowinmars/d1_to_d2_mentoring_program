using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AdvancedCSharp.Tests
{
    public abstract class TestsBase : IDisposable
    {
        protected static readonly string RootPath = Directory.GetCurrentDirectory();

        protected TestsBase()
        {
            foreach (var folderName in TestsBase.Folders)
            {
                Directory.CreateDirectory(Path.Combine(TestsBase.RootPath, folderName));
            }

            foreach (var filePath in Files)
            {
                var fileStream = File.Create(filePath);

                fileStream.Dispose();
            }
        }

        protected static IList<string> Folders { get; } = new[]
        {
            "testFolder",
            @"testFolder\first",
            @"testFolder\first\first",
            @"testFolder\first\second",
            @"testFolder\first\third",
            @"testFolder\first\forth",
            @"testFolder\second",
            @"testFolder\second\first",
            @"testFolder\second\second",
            @"testFolder\third",
            @"testFolder\forth",
            @"testFolder\forth\first",
            @"testFolder\forth\second",
            @"testFolder\forth\second\first",
            @"testFolder\forth\second\second",
            @"testFolder\forth\second\third",
            @"testFolder\forth\second\forth",
            @"testFolder\forth\third",
            @"testFolder\forth\forth",
        };

        protected static IList<string> Files { get; } = new[]
        {
            Path.Combine(Folders[1], "newFile.txt"),

            Path.Combine(Folders[2], "newFileA.txt"),
            Path.Combine(Folders[2], "newFileB.txt"),
            Path.Combine(Folders[2], "newFileC.txt"),
            Path.Combine(Folders[2], "newFileD.txt"),

            Path.Combine(Folders[7], "newFile1.txt"),
            Path.Combine(Folders[7], "aNewFile.txt"),

        };

        public void Dispose()
        {
            Directory.Delete(Path.Combine(TestsBase.RootPath, "testFolder"), recursive: true);
        }
    }
}