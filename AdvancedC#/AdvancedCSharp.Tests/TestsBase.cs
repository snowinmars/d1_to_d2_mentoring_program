using System;
using System.Collections.Generic;
using System.IO;

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

        protected static IDictionary<string, string> ExpectedResultsForNonDefaultFilter { get; } = new Dictionary<string, string>
        {
            {TestsBase.Folders[0], "D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\r\n"},
            {TestsBase.Folders[1], "D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\forth\r\n"},
            {TestsBase.Folders[2], ""},
            {TestsBase.Folders[3], ""},
            {TestsBase.Folders[4], ""},
            {TestsBase.Folders[5], ""},
            {TestsBase.Folders[6],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\\first\r\n"},
            {TestsBase.Folders[7], ""},
            {TestsBase.Folders[8], ""},
            {TestsBase.Folders[9], ""},
            {TestsBase.Folders[10], "D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\forth\r\n"},
            {TestsBase.Folders[11], ""},
            {TestsBase.Folders[12], "D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\forth\r\n"},
            {TestsBase.Folders[13], ""},
            {TestsBase.Folders[14], ""},
            {TestsBase.Folders[15], ""},
            {TestsBase.Folders[16], ""},
            {TestsBase.Folders[17], ""},
            {TestsBase.Folders[18], ""},
        };

        protected static IDictionary<string, string> ExpectedResultsForHardcodedFilter { get; } = new Dictionary
            <string, string>
        {
            {TestsBase.Folders[0],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\third\r\n"},
            {TestsBase.Folders[1],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\forth\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\second\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\third\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\newFile.txt\r\n"},
            {TestsBase.Folders[2],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\\newFileA.txt\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\\newFileB.txt\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\\newFileC.txt\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\first\\first\\newFileD.txt\r\n"},
            {TestsBase.Folders[3], ""},
            {TestsBase.Folders[4], ""},
            {TestsBase.Folders[5], ""},
            {TestsBase.Folders[6],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\\second\r\n"},
            {TestsBase.Folders[7],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\\first\\aNewFile.txt\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\second\\first\\newFile1.txt\r\n"},
            {TestsBase.Folders[8], ""},
            {TestsBase.Folders[9], ""},
            {TestsBase.Folders[10],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\forth\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\third\r\n"},
            {TestsBase.Folders[11], ""},
            {TestsBase.Folders[12],"D:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\first\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\forth\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\second\r\nD:\\prg\\github\\d1_to_d2_mentoring_program\\AdvancedC#\\AdvancedCSharp.Tests\\bin\\Debug\\testFolder\\forth\\second\\third\r\n"},
            {TestsBase.Folders[13], ""},
            {TestsBase.Folders[14], ""},
            {TestsBase.Folders[15], ""},
            {TestsBase.Folders[16], ""},
            {TestsBase.Folders[17], ""},
            {TestsBase.Folders[18], ""},
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