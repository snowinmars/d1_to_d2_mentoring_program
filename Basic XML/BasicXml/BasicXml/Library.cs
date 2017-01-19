using BasicXml.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace BasicXml
{
    public static class Library
    {
        private static readonly IEnumerable<string> ReservedNames = new[] { "Book", "Library", "TypeName" };

        private static readonly IDictionary<string, string[]> TypeAttributeBinding = new Dictionary<string, string[]>
        {
            { "Author", new [] {"FirstName", "SecondName",} },
        };

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

                                    if (reader.HasAttributes)
                                    {
                                        var attrs = Library.TypeAttributeBinding[reader.Name];
                                        Type type = Type.GetType($"BasicXml.{reader.Name}");

                                        if (type == null)
                                        {
                                            throw new Exception("Type is null");
                                        }

                                        var author = Activator.CreateInstance(type);

                                        foreach (var attr in attrs)
                                        {
                                            PropertyInfo propertyInfo = type.GetProperty(attr);
                                            propertyInfo.SetValue(author, reader.GetAttribute(attr));
                                        }

                                        PropertyInfo bookPropertyInfo = book.GetType().GetProperty($"{reader.Name}s");
                                        object collection = bookPropertyInfo.GetValue(book);
                                        MethodInfo addMeth = collection.GetType().GetMethod("Add");

                                        addMeth?.Invoke(collection, new [] { author });
                                    }
                                }
                                break;

                            case XmlNodeType.Text:
                                {
                                    PropertyInfo propertyInfo = book.GetType().GetProperty(name);

                                    if (propertyInfo == null)
                                    {
                                        continue;
                                    }

                                    if (propertyInfo.PropertyType.Name == "String")
                                    {
                                        propertyInfo.SetValue(book, reader.Value);
                                    }
                                    else
                                    {
                                        var tryParseMeth = propertyInfo.PropertyType.GetMethod("TryParse", new[] { typeof(string), propertyInfo.PropertyType.MakeByRefType() });
                                        var equalsMeth = propertyInfo.PropertyType.GetMethod("Equals", new[] { propertyInfo.PropertyType });

                                        object[] param = { reader.Value, null };
                                        tryParseMeth?.Invoke(null, param);

                                        if (param[1] != null &&
                                            !(bool)
                                                equalsMeth.Invoke(param[1],
                                                                    new[] { propertyInfo.PropertyType.GetDefaultValue() }))
                                        {
                                            propertyInfo.SetValue(book, param[1]);
                                        }
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

            foreach (var author in book.Authors)
            {
                writter.WriteStartElement("Author");

                writter.WriteAttributeString("FirstName", author.FirstName);
                writter.WriteAttributeString("SecondName", author.SecondName);

                writter.WriteEndElement();
            }

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