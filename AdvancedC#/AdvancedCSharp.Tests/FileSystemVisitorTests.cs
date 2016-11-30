using AdvancedCSharp.Core;
using AdvancedCSharp.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public void FSV_SearchByDefaultFilterNonRecursively_MustWorkAsDFS()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string fsvResult = fsv.SearchByFilter(fullFolderPath)
                                        .RepresentAsString();

                Assert.True(fsvResult.Equals(TestsBase.ExpectedResultsForDefaultFilterNonRecursively[TestsBase.Folders[i]]));

                ++i;
            }
        }

        [Fact]
        public void FSV_SearchByDefaultFilterRecursively_MustWorkAsDFS()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string fsvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: true)
                                        .RepresentAsString();

                Assert.True(fsvResult.Equals(TestsBase.ExpectedResultsForDefaultFilterRecursively[TestsBase.Folders[i]]));

                ++i;
            }
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterNonRecursively_MustWorkAsDFS()
        {
            const string letter = "f";
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith(letter, StringComparison.InvariantCultureIgnoreCase);

            FileSystemVisitor fsv = new FileSystemVisitor(filter);
            int i = 0;

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string fsvResult = fsv.SearchByFilter(fullFolderPath)
                                        .RepresentAsString();

                Assert.True(fsvResult.Equals(TestsBase.ExpectedResultsForNonDefaultFilterNonRecursively[TestsBase.Folders[i]]));

                ++i;
            }
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterRecursively_MustWorkAsDFS()
        {
            const string letter = "f";
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith(letter, StringComparison.InvariantCultureIgnoreCase);

            FileSystemVisitor fsv = new FileSystemVisitor(filter);

            int i = 0;

            foreach (var fullFolderPath in TestsBase.Folders
                                                    .Select(f => Path.Combine(TestsBase.RootPath, f)))
            {
                string fsvResult = fsv.SearchByFilter(fullFolderPath, isRecursive: true)
                                        .RepresentAsString();

                Assert.True(fsvResult.Equals(TestsBase.ExpectedResultsForNonDefaultFilterRecursively[TestsBase.Folders[i]]));

                ++i;
            }
        }

        [Fact]
        public void FSV_StartEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.Start += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.RootPath);

            Assert.True(i == 1);
        }

        [Fact]
        public void FSV_FinishEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.Finish += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.RootPath);

            Assert.True(i == 1);
        }

        [Fact]
        public void FSV_EventOrderByDefaultFilter_MustBeCorrect()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            StringBuilder result = new StringBuilder(64);

            fsv.Start += (sender, args) =>
            {
                result.Append("s");
            };
            fsv.DirectoryFound += (sender, args) =>
            {
                result.Append("df");
            };
            fsv.FileFound += (sender, args) =>
            {
                result.Append("ff");
            };
            fsv.FilteredDirectoryFound += (sender, args) =>
            {
                result.Append("fdf");
            };
            fsv.FilteredFileFound += (sender, args) =>
            {
                result.Append("fff");
            };
            fsv.Finish += (sender, args) =>
            {
                result.Append("f");
            };

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            string exp = "s df df df df ff f".Replace(" ", string.Empty);

            Assert.Equal(expected: exp, actual: result.ToString());
        }


            [Fact]
        public void FSV_EventOrderByNonDefaultFilter_MustBeCorrect()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => info.Name.Contains("o"));

            StringBuilder result = new StringBuilder(64);

            fsv.Start += (sender, args) =>
            {
                result.Append("s");
            };
            fsv.DirectoryFound += (sender, args) =>
            {
                result.Append("df");
            };
            fsv.FileFound += (sender, args) =>
            {
                result.Append("ff");
            };
            fsv.FilteredDirectoryFound += (sender, args) =>
            {
                result.Append("fdf");
            };
            fsv.FilteredFileFound += (sender, args) =>
            {
                result.Append("fff");
            };
            fsv.Finish += (sender, args) =>
            {
                result.Append("f");
            };

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            string exp = "s df df fdf df fdf df ff f".Replace(" ", string.Empty);

            Assert.Equal(expected: exp, actual: result.ToString());
        }

        [Fact]
        public void FSV_DirectoryFindedEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            int i = 0;

            fsv.DirectoryFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .First());

            Assert.True(i == 4);
        }

        [Fact]
        public void FSV_DirectoryFiltredFindedEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => info.Name.StartsWith("f", StringComparison.InvariantCultureIgnoreCase));

            int i = 0;

            fsv.FilteredDirectoryFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .First());

            Assert.True(i == 2);
        }

        [Fact]
        public void FSV_FileFindedEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            int i = 0;

            fsv.FileFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            Assert.True(i == 1);
        }

        [Fact]
        public void FSV_FileFiltredFindedEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => info.Name.StartsWith("n", StringComparison.InvariantCultureIgnoreCase));

            int i = 0;

            fsv.FileFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            Assert.True(i == 1);
        }

        #region interrupt

        [Fact]
        public void FSV_InterruptOnFindedDirectoryEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.DirectoryFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("s", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .First())
                                                    .ToList();

            Assert.True(result.Count == 3);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "second");
        }

        [Fact]
        public void FSV_InterruptOnFilterFindedDirectoryEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FilteredDirectoryFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("s", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .First())
                                                .ToList();

            Assert.True(result.Count == 3);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "second");
        }

        [Fact]
        public void FSV_InterruptOnFindedFileEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FileFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .ElementAt(2))
                                                .ToList();

            Assert.True(result.Count == 1);
            Assert.True(result.ElementAt(0).Name == "newFileA.txt");
        }

        [Fact]
        public void FSV_InterruptOnFilterFindedFileEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FilteredFileFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .ElementAt(2))
                                                .ToList();

            Assert.True(result.Count == 1);
            Assert.True(result.ElementAt(0).Name == "newFileA.txt");
        }

        [Fact]
        public void FSV_Event_MustWorkAfterInterrupt()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            int i = 0;
            int j = 0;

            fsv.FileFound += (sender, args) =>
            {
                ++j;
                args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
            };
            fsv.DirectoryFound += (sender, args) =>
            {
                ++j;
                args.State = FileSystemVisitorEventArgsStates.StopOnFirstFindedCoincidence;
            };
            fsv.Finish += (sender, args) =>
            {
                ++i;
            };

            fsv.SearchByFilter(TestsBase.TestFolderPath);

            Assert.True(i == 1);
            Assert.True(j == 1);
        }

        #endregion interrupt

        #region ignore

        [Fact]
        public void FSV_IgnoreOnFindedDirectoryEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.DirectoryFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("s", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.IgnoreThisEntry;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .First())
                                                        .ToList();

            Assert.True(result.Count == 3);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "third");
        }

        [Fact]
        public void FSV_IgnoreOnFilterFindedDirectoryEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FilteredDirectoryFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("s", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.IgnoreThisEntry;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .First())
                                                        .ToList();

            Assert.True(result.Count == 3);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "third");
        }

        [Fact]
        public void FSV_IgnoreOnFindedFileEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FileFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.IgnoreThisEntry;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .ElementAt(1))
                                                        .ToList();

            Assert.True(result.Count == 4);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "second");
            Assert.True(result.ElementAt(3).Name == "third");
        }

        [Fact]
        public void FSV_IgnoreOnFilterFindedFileEvent_MustWork()
        {
            FileSystemVisitor fsv = new FileSystemVisitor(info => true);

            fsv.FilteredFileFound += (sender, args) =>
            {
                if (args.Value.Name.StartsWith("n", StringComparison.InvariantCultureIgnoreCase))
                {
                    args.State = FileSystemVisitorEventArgsStates.IgnoreThisEntry;
                }
            };

            IList<FileSystemInfo> result = fsv.SearchByFilter(TestsBase.Folders
                                                                    .Select(f => Path.Combine(TestsBase.RootPath, f))
                                                                    .ElementAt(1))
                                                        .ToList();

            Assert.True(result.Count == 4);
            Assert.True(result.ElementAt(0).Name == "first");
            Assert.True(result.ElementAt(1).Name == "forth");
            Assert.True(result.ElementAt(2).Name == "second");
            Assert.True(result.ElementAt(3).Name == "third");
        }

        #endregion ignore

        #endregion positive

        #region negative

        [Fact]
        public void FSV_DirectoryFindedEvent_MustInvokeOnSearchingByDefaultFilter()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.DirectoryFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .First());

            Assert.True(i == 4);
        }

        [Fact]
        public void FSV_DirectoryFindedEvent_MustNotInvokeOnSearchingByDefaultFilter()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.FilteredDirectoryFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .First());

            Assert.True(i == 0);
        }

        [Fact]
        public void FSV_DirectoryFiltredFindedEvent_MustNotInvokeOnSearchingByDefaultFilter()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.FilteredDirectoryFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .First());

            Assert.True(i == 0);
        }

        [Fact]
        public void FSV_FileFindedEvent_MustNotInvokeOnSearchingByDefaultFilter()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.FilteredFileFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            Assert.True(i == 0);
        }

        [Fact]
        public void FSV_FileFiltredFindedEvent_MustNotInvokeOnSearchingByDefaultFilter()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            int i = 0;

            fsv.FilteredFileFound += (sender, args) => ++i;

            fsv.SearchByFilter(TestsBase.Folders
                                        .Select(f => Path.Combine(TestsBase.RootPath, f))
                                        .ElementAt(1));

            Assert.True(i == 0);
        }

        [Fact]
        public void FSV_SearchByDefaultFilterNonRecursively_MustNotReturnsNullOnTrashInput()
        {
            FileSystemVisitor fsv = new FileSystemVisitor();

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString());
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

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString());
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        [Fact]
        public void FSV_SearchByNonDefaultFilterRecursively_MustNotReturnsNullOnTrashInput()
        {
            Func<FileSystemInfo, bool> filter = fileInfo => fileInfo.Name.StartsWith("f", StringComparison.InvariantCultureIgnoreCase);
            FileSystemVisitor fsv = new FileSystemVisitor(filter);

            IEnumerable<FileSystemInfo> result = fsv.SearchByFilter(Guid.NewGuid().ToString());
            string stringResult = result.RepresentAsString();

            Assert.NotNull(result);
            Assert.NotNull(stringResult);
        }

        #endregion negative
    }
}