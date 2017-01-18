using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandS.Algorithm.Library.GeneratorNamespace;

namespace BasicXml.Common
{
    public static class Constant
    {
        static Constant()
        {
            DataFileFullPath = @"D:\file.dat";
            Encoding = Encoding.UTF8;
            Isbn10Generator = new Isbn10Generator();
            TextGenerator = new TextGenerator()
            {
                IsFirstLetterAlwaysUpper = true,
            };
            Random = new Random();
        }

        public static string DataFileFullPath { get; }
        public static Encoding Encoding { get; }

        public static Isbn10Generator Isbn10Generator { get; }
        public static TextGenerator TextGenerator { get; }
        public static Random Random { get; }
    }
}
