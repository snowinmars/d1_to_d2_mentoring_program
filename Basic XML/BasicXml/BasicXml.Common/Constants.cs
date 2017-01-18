using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandS.Algorithm.Library.GeneratorNamespace;

namespace BasicXml.Common
{
    public static class Constants
    {
        public static string BookTypeName = "Book";
        public static string FullPathToDataFile = @"D:\file.dat";
        public static Isbn10Generator Isbn10Generator = new Isbn10Generator();
        public static TextGenerator TextGenerator = new TextGenerator();
        public static Random Random = new Random();
    }
}
