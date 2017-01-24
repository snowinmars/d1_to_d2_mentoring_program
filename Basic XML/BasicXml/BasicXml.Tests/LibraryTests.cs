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

            using (var stream = File.OpenRead(Constants.FullPathToDataFile))
                Library.GetAll(stream);
        }

        [Fact]
        public void AddAll_MustNot_ThrowAnyException()
        {
            using (var stream = File.OpenWrite(Constants.FullPathToDataFile))
            {    Library.AddAll(new List<LibraryItem>
            {
                EntityGenerator.GetNewBook(),
                EntityGenerator.GetNewNewspaper(),
                EntityGenerator.GetNewPatent(),
                EntityGenerator.GetNewPatent(),
                EntityGenerator.GetNewBook(),
                EntityGenerator.GetNewNewspaper(),
            }, stream);}
        }
    }
}
