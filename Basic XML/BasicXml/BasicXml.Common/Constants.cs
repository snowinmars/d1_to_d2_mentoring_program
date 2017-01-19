using SandS.Algorithm.Library.GeneratorNamespace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicXml.Common
{
    public static class Constants
    {
        public const string BookTypeName = "Book";
        public const string FullPathToDataFile = @"D:\file.dat";
        public const string Namespace = "BasicXml";
        public const string NewspaperTypeName = "Newspaper";
        public const string PatentName = "Patent";
        public static readonly IEnumerable<string> IgnoredTags;
        public static readonly Isbn10Generator Isbn10Generator;
        public static readonly IssnGenerator IssnGenerator;
        public static readonly Random Random;
        public static readonly TextGenerator TextGenerator;
        public static readonly IDictionary<string, string[]> TypeAttributeBinding;
        public static readonly IEnumerable<string> LibraryItemTags;

        static Constants()
        {
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