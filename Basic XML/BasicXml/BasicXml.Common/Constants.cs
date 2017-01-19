using SandS.Algorithm.Library.GeneratorNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BasicXml.Common
{
    public static class Constants
    {
        public const string BookTypeName = "Book";
        public const string FullPathToDataFile = @"D:\file.dat";
        public const string Namespace = "BasicXml";
        public const string NewspaperTypeName = "Newspaper";
        public const string PatentName = "Patent";
        public static IEnumerable<string> IgnoredTags { get; }
        public static Isbn10Generator Isbn10Generator { get; }
        public static IssnGenerator IssnGenerator { get; }
        public static Random Random { get; }
        public static TextGenerator TextGenerator { get; }
        public static IDictionary<string, string[]> TypeAttributeBinding { get; }
        public static IEnumerable<string> LibraryItemTags { get; }
        public static XmlReaderSettings XmlReaderSettings { get; }
        public static XmlWriterSettings XmlWriterSettings { get; }

        static Constants()
        {
            Constants.XmlWriterSettings = new XmlWriterSettings { Indent = true, };
            Constants.XmlReaderSettings = new XmlReaderSettings { IgnoreWhitespace = true, };

            Constants.LibraryItemTags = new List<string>
            {
                "Book",
                "Newspaper",
                "Patent",
            };

            Constants.TypeAttributeBinding = new Dictionary<string, string[]>
            {
                { "Author", new [] {
                    "FirstName",
                    "SecondName",
                } },
            };

            Constants.IgnoredTags = new[]
            {
                "Library",
                "TypeName",
            }.Concat(Constants.LibraryItemTags);

            Constants.Random = new Random();
            Constants.TextGenerator = new TextGenerator();
            Constants.IssnGenerator = new IssnGenerator();
            Constants.Isbn10Generator = new Isbn10Generator();
        }
    }
}