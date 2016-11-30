using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AdvancedCSharp.Tests
{
    public abstract class TestsBase : IDisposable
    {
        private const string testFolderName = "testFolder";
        public static readonly string RootPath = Directory.GetCurrentDirectory();

        protected TestsBase()
        {
            foreach (var folderName in TestsBase.Folders)
            {
                Directory.CreateDirectory(Path.Combine(TestsBase.RootPath, folderName));
            }

            foreach (var filePath in TestsBase.Files)
            {
                var fileStream = File.Create(filePath);

                fileStream.Dispose();
            }
        }

        protected static IList<string> Folders { get; } = new[]
        {
            TestsBase.testFolderName,
            Path.Combine(TestsBase.testFolderName, @"first"),
            Path.Combine(TestsBase.testFolderName, @"first\first"),
            Path.Combine(TestsBase.testFolderName, @"first\second"),
            Path.Combine(TestsBase.testFolderName, @"first\third"),
            Path.Combine(TestsBase.testFolderName, @"first\forth"),
            Path.Combine(TestsBase.testFolderName, @"second"),
            Path.Combine(TestsBase.testFolderName, @"second\first"),
            Path.Combine(TestsBase.testFolderName, @"second\second"),
            Path.Combine(TestsBase.testFolderName, @"third"),
            Path.Combine(TestsBase.testFolderName, @"forth"),
            Path.Combine(TestsBase.testFolderName, @"forth\first"),
            Path.Combine(TestsBase.testFolderName, @"forth\second"),
            Path.Combine(TestsBase.testFolderName, @"forth\second\first"),
            Path.Combine(TestsBase.testFolderName, @"forth\second\second"),
            Path.Combine(TestsBase.testFolderName, @"forth\second\third"),
            Path.Combine(TestsBase.testFolderName, @"forth\second\forth"),
            Path.Combine(TestsBase.testFolderName, @"forth\third"),
            Path.Combine(TestsBase.testFolderName, @"forth\forth"),
        };

        protected static IEnumerable<string> Files { get; } = new[]
        {
            Path.Combine(TestsBase.Folders[1], "newFile.txt"),

            Path.Combine(TestsBase.Folders[2], "newFileA.txt"),
            Path.Combine(TestsBase.Folders[2], "newFileB.txt"),
            Path.Combine(TestsBase.Folders[2], "newFileC.txt"),
            Path.Combine(TestsBase.Folders[2], "newFileD.txt"),

            Path.Combine(TestsBase.Folders[7], "newFile1.txt"),
            Path.Combine(TestsBase.Folders[7], "aNewFile.txt"),
        };

        public void Dispose()
        {
            Directory.Delete(Path.Combine(TestsBase.RootPath, "testFolder"), recursive: true);
        }
    }
}