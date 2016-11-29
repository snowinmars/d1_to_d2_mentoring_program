using System;
using System.Collections.Generic;
using System.IO;

namespace AdvancedCSharp.Tests
{
    public abstract class TestsBase : IDisposable
    {
        protected static readonly string RootPath = Directory.GetCurrentDirectory();

        protected TestsBase()
        {
            foreach (var folder in TestsBase.Folders)
            {
                Directory.CreateDirectory(Path.Combine(TestsBase.RootPath, folder));
            }
        }

        protected static IEnumerable<string> Folders { get; } = new[]
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

        public void Dispose()
        {
            Directory.Delete(Path.Combine(TestsBase.RootPath, "testFolder"), recursive: true);
        }
    }
}