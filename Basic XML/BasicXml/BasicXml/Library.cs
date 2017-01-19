﻿using BasicXml.Common;
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
        private static readonly IEnumerable<string> IgnoredTags = new[]
        {
            "Book",
            "Library",
            "TypeName",
        };

        private static readonly IDictionary<string, string[]> TypeAttributeBinding = new Dictionary<string, string[]>
        {
            { "Author", new [] {
                                    "FirstName",
                                    "SecondName",
                                } },
        };

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
                            Library.AddBook(writter, item as Book);
                        }
                    }

                    writter.WriteEndElement();
                    writter.WriteEndDocument();
                }
            }
        }

        public static IEnumerable<LibraryItem> GetAll()
        {
            using (var stream = File.OpenRead(Constants.FullPathToDataFile))
            {
                using (var reader = XmlReader.Create(stream))
                {
                    return Library.ReadAllLibraryItems(reader);
                }
            }
        }

        private static void AddBook(XmlWriter writter, Book book)
        {
            writter.WriteStartElement("Book"); // <Book>

            writter.WriteTag("Id", book.Id); // <Id>...</Id>
            writter.WriteTag("TypeName", book.TypeName); // <TypeName>...</TypeName>
            writter.WriteTag("PageNumber", book.PageNumber); // <PageNumber>...</PageNumber>
            writter.WriteTag("Title", book.Title); // <Title>...</Title>
            writter.WriteTag("Annotation", book.Annotation); // <Annotation>...</Annotation>
            writter.WriteTag("CityName", book.CityName); // <CityName>...</CityName>
            writter.WriteTag("Isbn", book.Isbn); // <Isbn>...</Isbn>
            writter.WriteTag("Year", book.Year); // <Year>...</Year>

            foreach (var author in book.Authors) // <Author FirstName="..." SecondName="..." />
            {
                writter.WriteStartElement("Author");

                writter.WriteAttributeString("FirstName", author.FirstName);
                writter.WriteAttributeString("SecondName", author.SecondName);

                writter.WriteEndElement();
            }

            writter.WriteEndElement(); // </Book>
        }

        /// <summary>
        /// Create instance of type and fill it's properties with attributes
        /// </summary>
        /// <param name="reader">reader provides value for attributes</param>
        /// <param name="attrs">names of attributes</param>
        /// <param name="type">create instance of this type</param>
        /// <returns></returns>
        private static object Create(XmlReader reader, string[] attrs, Type type)
        {
            var item = Activator.CreateInstance(type);

            foreach (var attr in attrs)
            {
                PropertyInfo propertyInfo = type.GetProperty(attr);
                propertyInfo.SetValue(item, reader.GetAttribute(attr));
            }

            return item;
        }

        private static void HandleAttribute(XmlReader reader, Book book)
        {
            var attrs = Library.TypeAttributeBinding[reader.Name]; // I will work only with these attributes
            Type type = Type.GetType($"BasicXml.{reader.Name}"); // TODO fix namespace

            if (type == null)
            {
                throw new Exception("Type is null");
            }

            object item = Library.Create(reader, attrs, type);

            PropertyInfo bookPropertyInfo = book.GetType().GetProperty($"{reader.Name}s"); // collection
            object collection = bookPropertyInfo.GetValue(book); // instance of collection
            MethodInfo addMeth = collection.GetType().GetMethod("Add"); // method of instance of collection

            addMeth?.Invoke(collection, new[] { item }); // add item to the collection
        }

        private static void HandleText(XmlReader reader, Book book, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.Name == "String")
            {
                propertyInfo.SetValue(book, reader.Value);
                return;
            }

            // bool MyType.TryParse(string, out MyType)
            MethodInfo tryParseMeth = propertyInfo.PropertyType
                                                    .GetMethod("TryParse", new[] { typeof(string), propertyInfo.PropertyType.MakeByRefType() });

            // bool Equals(this MyType, MyType)
            MethodInfo equalsMeth = propertyInfo.PropertyType
                                                    .GetMethod("Equals", new[] { propertyInfo.PropertyType });

            object[] param = { reader.Value, null }; // in null here after Invoke() will appear out value
            tryParseMeth?.Invoke(null, param);

            // there was some troubles here with bool result of TryParse method, so I use this way
            if (param[1] != null && // if parse was success for class...
                !(bool)
                    equalsMeth.Invoke(param[1],
                        new[] { propertyInfo.PropertyType.GetDefaultValue() })) // ...and for struct
            {
                propertyInfo.SetValue(book, param[1]);
            }
        }

        private static IList<LibraryItem> ReadAllLibraryItems(XmlReader reader)
        {
            IList<LibraryItem> result = new List<LibraryItem>(32);

            Book book = new Book();
            string name = ""; // not null attribute

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.Name == "Book") // if I read Book tag to the end
                        {
                            result.Add(book);

                            book = new Book();
                        }
                        break;

                    case XmlNodeType.Element:
                        if (!Library.IgnoredTags.Contains(reader.Name)) // if node is not some trash
                        {
                            name = reader.Name; // work with this node on the next itteration

                            if (reader.HasAttributes) // but if node has attributes, try to write it down
                            {
                                // I will invoke this if block as many times, as how many attributes are in tag
                                Library.HandleAttribute(reader, book);
                            }
                        }
                        break;

                    case XmlNodeType.Text:
                        PropertyInfo propertyInfo = book.GetType().GetProperty(name);

                        if (propertyInfo == null)
                        {
                            continue;
                        }

                        Library.HandleText(reader, book, propertyInfo);
                        break;
                }
            }
            return result;
        }

        private static void WriteTag(this XmlWriter writter, string name, object value)
        {
            writter.WriteStartElement(name);
            writter.WriteValue(value.ToString());
            writter.WriteEndElement();
        }
    }
}