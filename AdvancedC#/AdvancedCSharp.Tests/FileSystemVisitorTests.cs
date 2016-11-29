using AdvancedCSharp.Core;
using AdvancedCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace AdvancedCSharp.Tests
{
    public class FileSystemVisitorTests : TestsBase
    {
        #region positive

        [Fact]
        public void FSV_DefaultCtor_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            Assert.True(fsv.Filter == FileSystemVisitor.DefaultFilter);
        }

        [Fact]
        // [InlineData((info) => true)] cant use attributer here due to compiler convert lambda expression.
        // [InlineData((info) => info.)]
        public void FSV_ParameterCtor_MustWork()
        {
            Func<FileSystemInfo, bool>[] funcs =
            {
                info => true,
                info => info.Name.StartsWith("A", StringComparison.InvariantCultureIgnoreCase),
            };

            foreach (var func in funcs)
            {
                FileSystemVisitor fsv = new FileSystemVisitor(func);

                Assert.True(fsv.Filter == func);
            }
        }

        [Fact]
        public void FSV_SearchByDefaultFilterNonRecursively_MustWorkAsPowershell()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();
            PowershellSearchEngine pse = new PowershellSearchEngine();

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string pseResult = pse.Ls(fullFolderPath, isRecursive: false)
                                        .RepresentAsString();
                string sfvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: false)
                                        .RepresentAsString();

                Assert.Equal(expected: pseResult, actual: sfvResult);
            }
        }

        [Fact]
        public void FSV_SearchByDefaultFilterRecursively_MustWorkAsPowershell()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();
            PowershellSearchEngine pse = new PowershellSearchEngine();

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string pseResult = pse.Ls(fullFolderPath, isRecursive: true)
                                        .RepresentAsString();
                string sfvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: true)
                                        .RepresentAsString();

                Assert.Equal(expected: pseResult, actual: sfvResult);
            }
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterNonRecursively_MustWorkAsPowershell()
        {
            const string letter = "f";
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith(letter, StringComparison.InvariantCultureIgnoreCase);

            FileSystemVisitor fsv = new FileSystemVisitor(filter);
            PowershellSearchEngine pse = new PowershellSearchEngine();

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string pseResult = pse.Ls(fullFolderPath, isRecursive: false, filenameStartsWith: letter)
                                        .RepresentAsString();
                string sfvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: false)
                                        .RepresentAsString();

                Assert.Equal(expected: pseResult, actual: sfvResult);
            }
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterRecursively_MustWorkAsPowershell()
        {
            const string letter = "f";
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith(letter, StringComparison.InvariantCultureIgnoreCase);

            FileSystemVisitor fsv = new FileSystemVisitor(filter);
            PowershellSearchEngine pse = new PowershellSearchEngine();

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string pseResult = pse.Ls(fullFolderPath, isRecursive: true, filenameStartsWith: letter)
                                        .RepresentAsString();
                string sfvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: true)
                                        .RepresentAsString();

                Assert.Equal(expected: pseResult, actual: sfvResult);
            }
        }

        #endregion positive

        #region negative

        [Fact]
        public void FSV_SearchByDefaultFilterNonRecursively_MustNotReturnsNullOnTrashInput()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString(), isRecursive: false);
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        [Fact]
        public void FSV_SearchByDefaultFilterRecursively_MustNotReturnsNullOnTrashInput()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString(), isRecursive: true);
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterNonRecursively_MustNotReturnsNullOnTrashInput()
        {
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith("f", StringComparison.InvariantCultureIgnoreCase);
            FileSystemVisitor fsv = new FileSystemVisitor(filter);

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString(), isRecursive: false);
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterRecursively_MustNotReturnsNullOnTrashInput()
        {
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith("f", StringComparison.InvariantCultureIgnoreCase);
            FileSystemVisitor fsv = new FileSystemVisitor(filter);

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString(), isRecursive: false);
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        #endregion negative
    }
}