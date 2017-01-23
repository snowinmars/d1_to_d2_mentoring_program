using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;
using BasicXml.Generators;
using Xunit;

namespace BasicXml.Tests
{
    public class LibraryTests
    {
        [Fact]
        public void GetAll_MustNot_ThrowAnyException()
        {
            Library.GetAll();

            if (File.Exists(Constants.FullPathToDataFile))
            {
                File.Delete(Constants.FullPathToDataFile);
            }

            Library.GetAll();
        }

        [Fact]
        public void AddAll_MustNot_ThrowAnyException()
        {
            Library.AddAll(new List<LibraryItem> { EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper() });

            if (File.Exists(Constants.FullPathToDataFile))
            {
                File.Delete(Constants.FullPathToDataFile);
            }

            Library.AddAll(new List<LibraryItem> { EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewPatent(), EntityGenerator.GetNewBook(), EntityGenerator.GetNewNewspaper() });

        }
    }
}
