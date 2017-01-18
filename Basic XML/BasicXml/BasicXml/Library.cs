using BasicXml.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace BasicXml
{
    public static class TypeExtensions
    {
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && 
                Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }
    }

    public static class Library
    {
        private static string FullPathToDataFile = @"D:\file.dat";
        private static IEnumerable<string> ReservedNames = new[] { "Book", "Library", "TypeName" };

        public static IEnumerable<LibraryItem> GetAll()
        {
            IList<LibraryItem> result = new List<LibraryItem>(32);

            using (var stream = File.OpenRead(Constants.FullPathToDataFile))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    Book book = new Book();
                    string name = "";
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.EndElement:
                                if (reader.Name == "Book")
                                {
                                    result.Add(book);

                                    book = new Book();
                                }
                                break;
                            case XmlNodeType.Element:
                                if (!Library.ReservedNames.Contains(reader.Name))
                                {
                                    name = reader.Name;
                                }
                                break;
                            case XmlNodeType.Text:
                                {
                                    PropertyInfo propertyInfo = book.GetType().GetProperty(name);

                                    //if (propertyInfo.PropertyType.Name == "Guid")
                                    //{
                                    //    Guid g;

                                    //    if (Guid.TryParse(reader.Value, out g))
                                    //    {
                                    //        v = g;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    v = reader.Value;
                                    //}

                                    var tryParseMeth = propertyInfo.PropertyType.GetMethod("TryParse", new[] {typeof (string), propertyInfo.PropertyType.MakeByRefType()});

                                    object[] param = { reader.Value, null };
                                    tryParseMeth.Invoke(null, param);

                                    if (param[1] != propertyInfo.PropertyType.GetDefaultValue())
                                    {
                                        propertyInfo.SetValue(book, param[1]);
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            return result;
        }

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