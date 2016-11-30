using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedCSharp.Tests
{
    public abstract class TestsBase : IDisposable
    {
        private const string TestFolderName = "testFolder";
        internal static readonly string RootPath = Directory.GetCurrentDirectory();

        private static readonly string TestFolderPath = Path.Combine(TestsBase.RootPath, TestsBase.TestFolderName);

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

        static TestsBase()
        {
            TestsBase.Folders = new[]
            {
                TestsBase.TestFolderName,
                Path.Combine(TestsBase.TestFolderName, @"first"),
                Path.Combine(TestsBase.TestFolderName, @"first\first"),
                Path.Combine(TestsBase.TestFolderName, @"first\second"),
                Path.Combine(TestsBase.TestFolderName, @"first\third"),
                Path.Combine(TestsBase.TestFolderName, @"first\forth"),
                Path.Combine(TestsBase.TestFolderName, @"second"),
                Path.Combine(TestsBase.TestFolderName, @"second\first"),
                Path.Combine(TestsBase.TestFolderName, @"second\second"),
                Path.Combine(TestsBase.TestFolderName, @"third"),
                Path.Combine(TestsBase.TestFolderName, @"forth"),
                Path.Combine(TestsBase.TestFolderName, @"forth\first"),
                Path.Combine(TestsBase.TestFolderName, @"forth\second"),
                Path.Combine(TestsBase.TestFolderName, @"forth\second\first"),
                Path.Combine(TestsBase.TestFolderName, @"forth\second\second"),
                Path.Combine(TestsBase.TestFolderName, @"forth\second\third"),
                Path.Combine(TestsBase.TestFolderName, @"forth\second\forth"),
                Path.Combine(TestsBase.TestFolderName, @"forth\third"),
                Path.Combine(TestsBase.TestFolderName, @"forth\forth"),
            };

            TestsBase.ExpectedResultsForNonDefaultFilter = new Dictionary<string, string>
            {
                {TestsBase.Folders[0], $"{Path.Combine(TestsBase.TestFolderPath, "first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth")}{Environment.NewLine}"},
                {TestsBase.Folders[1], $"{Path.Combine(TestsBase.TestFolderPath, "first\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\forth")}{Environment.NewLine}"},
                {TestsBase.Folders[2], ""},
                {TestsBase.Folders[3], ""},
                {TestsBase.Folders[4], ""},
                {TestsBase.Folders[5], ""},
                {TestsBase.Folders[6], $"{Path.Combine(TestsBase.TestFolderPath, "second\\first")}{Environment.NewLine}"},
                {TestsBase.Folders[7], ""},
                {TestsBase.Folders[8], ""},
                {TestsBase.Folders[9], ""},
                {TestsBase.Folders[10], $"{Path.Combine(TestsBase.TestFolderPath, "forth\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\forth")}{Environment.NewLine}"},
                {TestsBase.Folders[11], ""},
                {TestsBase.Folders[12], $"{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\forth")}{Environment.NewLine}"},
                {TestsBase.Folders[13], ""},
                {TestsBase.Folders[14], ""},
                {TestsBase.Folders[15], ""},
                {TestsBase.Folders[16], ""},
                {TestsBase.Folders[17], ""},
                {TestsBase.Folders[18], ""},
            };

            TestsBase.ExpectedResultsForHardcodedFilter = new Dictionary<string, string>
            {
                {TestsBase.Folders[0], $"{Path.Combine(TestsBase.TestFolderPath, "first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "second")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "third")}{Environment.NewLine}"},
                {TestsBase.Folders[1], $"{Path.Combine(TestsBase.TestFolderPath, "first\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\forth")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\second")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\third")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\newFile.txt")}{Environment.NewLine}"},
                {TestsBase.Folders[2], $"{Path.Combine(TestsBase.TestFolderPath, "first\\first\\newFileA.txt")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\first\\newFileB.txt")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\first\\newFileC.txt")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "first\\first\\newFileD.txt")}{Environment.NewLine}"},
                {TestsBase.Folders[3], ""},
                {TestsBase.Folders[4], ""},
                {TestsBase.Folders[5], ""},
                {TestsBase.Folders[6], $"{Path.Combine(TestsBase.TestFolderPath, "second\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "second\\second")}{Environment.NewLine}"},
                {TestsBase.Folders[7], $"{Path.Combine(TestsBase.TestFolderPath, "second\\first\\aNewFile.txt")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "second\\first\\newFile1.txt")}{Environment.NewLine}"},
                {TestsBase.Folders[8], ""},
                {TestsBase.Folders[9], ""},
                {TestsBase.Folders[10], $"{Path.Combine(TestsBase.TestFolderPath, "forth\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\forth")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\second")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\third")}{Environment.NewLine}"},
                {TestsBase.Folders[11], ""},
                {TestsBase.Folders[12], $"{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\first")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\forth")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\second")}{Environment.NewLine}{Path.Combine(TestsBase.TestFolderPath, "forth\\second\\third")}{Environment.NewLine}"},
                {TestsBase.Folders[13], ""},
                {TestsBase.Folders[14], ""},
                {TestsBase.Folders[15], ""},
                {TestsBase.Folders[16], ""},
                {TestsBase.Folders[17], ""},
                {TestsBase.Folders[18], ""},
            };

            TestsBase.Files = new[]
            {
                Path.Combine(TestsBase.Folders[1], "newFile.txt"),

                Path.Combine(TestsBase.Folders[2], "newFileA.txt"),
                Path.Combine(TestsBase.Folders[2], "newFileB.txt"),
                Path.Combine(TestsBase.Folders[2], "newFileC.txt"),
                Path.Combine(TestsBase.Folders[2], "newFileD.txt"),

                Path.Combine(TestsBase.Folders[7], "newFile1.txt"),
                Path.Combine(TestsBase.Folders[7], "aNewFile.txt"),
            };
        }

        protected static IList<string> Folders { get; }

        protected static IDictionary<string, string> ExpectedResultsForNonDefaultFilter { get; }

        protected static IDictionary<string, string> ExpectedResultsForHardcodedFilter { get; }

        protected static IEnumerable<string> Files { get; }

        public void Dispose()
        {
            Directory.Delete(Path.Combine(TestsBase.RootPath, TestsBase.TestFolderName), recursive: true);
        }
    }
}