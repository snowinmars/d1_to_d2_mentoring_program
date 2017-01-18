using BasicXml.Common;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BasicXml
{
    public static class Library
    {
        private static string FullPathToDataFile = @"D:\file.dat";

        public static void AddAll(IEnumerable<LibraryItem> items)
        {
            using (var stream = File.OpenWrite(Constants.FullPathToDataFile))
            {
                using (XmlWriter writter = XmlWriter.Create(stream))
                {
                    writter.WriteStartDocument();
                    writter.WriteStartElement("Library");

                    foreach (var item in items)
                    {
                        if (item.TypeName == Constants.BookTypeName)
                        {
                            AddBook(writter, item as Book);
                        }
                    }

                    writter.WriteEndElement();
                    writter.WriteEndDocument();
                }
            }
        }

        private static void AddBook(XmlWriter writter, Book book)
        {
            writter.WriteStartElement("Book");

            writter.WriteTag("Id", book.Id);
            writter.WriteTag("TypeName", book.TypeName);
            writter.WriteTag("PageNumber", book.PageNumber);
            writter.WriteTag("Title", book.Title);
            writter.WriteTag("Annotation", book.Annotation);
            writter.WriteTag("CityName", book.CityName);
            writter.WriteTag("Isbn", book.Isbn);
            writter.WriteTag("Year", book.Year);

            writter.WriteStartElement("Authors");
            foreach (var author in book.Authors)
            {
                writter.WriteStartElement("Author");

                writter.WriteTag("FirstName", author.FirstName);
                writter.WriteTag("SecondName", author.SecondName);

                writter.WriteEndElement();
            }
            writter.WriteEndElement();

            writter.WriteEndElement();
        }

        private static void WriteTag(this XmlWriter writter, string name, object value)
        {
            writter.WriteStartElement(name);
            writter.WriteValue(value.ToString());
            writter.WriteEndElement();
        }
    }
}