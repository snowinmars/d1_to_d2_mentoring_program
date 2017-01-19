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
        public static void AddAll(IEnumerable<LibraryItem> items)
        {
            if (File.Exists(Constants.FullPathToDataFile))
            {
                File.Delete(Constants.FullPathToDataFile);
            }

            using (var stream = File.OpenWrite(Constants.FullPathToDataFile))
            {
                using (XmlWriter writter = XmlWriter.Create(stream))
                {
                    writter.WriteStartDocument();
                    writter.WriteStartElement(nameof(Library));

                    foreach (var item in items)
                    {
                        Library.WriteLibraryItem(writter, item);
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
            writter.WriteStartElement(nameof(Book)); // <Book>

            writter.WriteTag(nameof(LibraryItem.Id), book.Id); // <Id>...</Id>
            writter.WriteTag(nameof(LibraryItem.TypeName), book.TypeName); // <TypeName>...</TypeName>
            writter.WriteTag(nameof(LibraryItem.PageNumber), book.PageNumber); // <PageNumber>...</PageNumber>
            writter.WriteTag(nameof(LibraryItem.Title), book.Title); // <Title>...</Title>
            writter.WriteTag(nameof(book.Annotation), book.Annotation); // <Annotation>...</Annotation>
            writter.WriteTag(nameof(book.CityName), book.CityName); // <CityName>...</CityName>
            writter.WriteTag(nameof(book.Isbn), book.Isbn); // <Isbn>...</Isbn>
            writter.WriteTag(nameof(book.Year), book.Year); // <Year>...</Year>

            foreach (var author in book.Authors) // <Author FirstName="..." SecondName="..." />
            {
                Library.WriteAuthor(writter, author);
            }

            writter.WriteEndElement(); // </Book>
        }

        private static void AddNewspaper(XmlWriter writter, Newspaper newspaper)
        {
            writter.WriteStartElement(nameof(Newspaper)); // <Newspaper>

            writter.WriteTag(nameof(LibraryItem.Id), newspaper.Id); // <Id>...</Id>
            writter.WriteTag(nameof(LibraryItem.TypeName), newspaper.TypeName); // <TypeName>...</TypeName>
            writter.WriteTag(nameof(LibraryItem.PageNumber), newspaper.PageNumber); // <PageNumber>...</PageNumber>
            writter.WriteTag(nameof(LibraryItem.Title), newspaper.Title); // <Title>...</Title>
            writter.WriteTag(nameof(newspaper.CityName), newspaper.CityName); // <CityName>...</CityName>
            writter.WriteTag(nameof(newspaper.Issn), newspaper.Issn); // <Issn>...</Issn>
            writter.WriteTag(nameof(newspaper.Year), newspaper.Year); // <Year>...</Year>
            writter.WriteTag(nameof(newspaper.Date), newspaper.Date); // <Date>...</Date>
            writter.WriteTag(nameof(newspaper.Number), newspaper.Number); // <Number>...</Number>
            writter.WriteTag(nameof(newspaper.PublisherName), newspaper.PublisherName); // <PublisherName>...</PublisherName>

            writter.WriteEndElement(); // </Newspaper>
        }

        private static void AddPatent(XmlWriter writter, Patent patent)
        {
            writter.WriteStartElement(nameof(Patent)); // <Patent>

            writter.WriteTag(nameof(LibraryItem.Id), patent.Id); // <Id>...</Id>
            writter.WriteTag(nameof(LibraryItem.TypeName), patent.TypeName); // <TypeName>...</TypeName>
            writter.WriteTag(nameof(LibraryItem.PageNumber), patent.PageNumber); // <PageNumber>...</PageNumber>
            writter.WriteTag(nameof(LibraryItem.Title), patent.Title); // <Title>...</Title>
            writter.WriteTag(nameof(patent.Annotation), patent.Annotation); // <Annotation>...</Annotation>
            writter.WriteTag(nameof(patent.Country), patent.Country); // <Country>...</Country>
            writter.WriteTag(nameof(patent.FilingDate), patent.FilingDate); // <FilingDate>...</FilingDate>
            writter.WriteTag(nameof(patent.PublishingDate), patent.PublishingDate); // <PublishingDate>...</PublishingDate>
            writter.WriteTag(nameof(patent.RegistrationNumber), patent.RegistrationNumber); // <RegistrationNumber>...</RegistrationNumber>

            foreach (var author in patent.Authors) // <Author FirstName="..." SecondName="..." />
            {
                Library.WriteAuthor(writter, author);
            }

            writter.WriteEndElement(); // </Patent>
        }

        /// <summary>
        /// Create instance of type and fill it's properties with attributes
        /// </summary>
        /// <param name="reader">reader provides value for attributes</param>
        /// <param name="attrs">names of attributes</param>
        /// <param name="type">create instance of this type</param>
        /// <returns></returns>
        private static object Create(XmlReader reader, IEnumerable<string> attrs, Type type)
        {
            var item = Activator.CreateInstance(type);

            foreach (var attr in attrs)
            {
                PropertyInfo propertyInfo = type.GetProperty(attr);
                propertyInfo.SetValue(item, reader.GetAttribute(attr));
            }

            return item;
        }

        private static void HandleAttribute(XmlReader reader, LibraryItem libraryItem)
        {
            IEnumerable<string> attrs = Constants.TypeAttributeBinding[reader.Name]; // I will work only with these attributes
            Type type = Type.GetType($"{Constants.Namespace}.{reader.Name}"); // TODO fix namespace

            if (type == null)
            {
                throw new InvalidOperationException("Type is null");
            }

            object item = Library.Create(reader, attrs, type);

            PropertyInfo propertyInfo = libraryItem.GetType().GetProperty($"{reader.Name}s"); // collection
            object collection = propertyInfo.GetValue(libraryItem); // instance of collection
            MethodInfo addMethod = collection.GetType().GetMethod("Add"); // method of instance of collection

            addMethod?.Invoke(collection, new[] { item }); // add item to the collection
        }

        private static void HandleText(XmlReader reader, LibraryItem book, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.Name == nameof(String))
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

            LibraryItem libraryItem = null;
            string name = ""; // not null attribute

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (Constants.LibraryItemTags.Contains(reader.Name)) // if I read Book tag to the end
                        {
                            result.Add(libraryItem);

                            libraryItem = null;
                        }
                        break;

                    case XmlNodeType.Element:
                        if (Constants.LibraryItemTags.Contains(reader.Name))
                        {
                            libraryItem = Activator.CreateInstance(Type.GetType($"{Constants.Namespace}.{reader.Name}")) as LibraryItem;
                        }

                        if (!Constants.IgnoredTags.Contains(reader.Name)) // if node is not some trash
                        {
                            name = reader.Name; // work with this node on the next itteration

                            if (reader.HasAttributes) // but if node has attributes, try to write it down
                            {
                                // I will invoke this if block as many times, as how many attributes are in tag
                                Library.HandleAttribute(reader, libraryItem);
                            }
                        }
                        break;

                    case XmlNodeType.Text:
                        if (libraryItem == null)
                        {
                            throw new InvalidOperationException("Library item is null, check XmlNodeType.Element case");
                        }

                        PropertyInfo propertyInfo = libraryItem.GetType().GetProperty(name);

                        if (propertyInfo == null)
                        {
                            continue;
                        }

                        Library.HandleText(reader, libraryItem, propertyInfo);
                        break;
                }
            }
            return result;
        }

        private static void WriteAuthor(XmlWriter writter, Author author)
        {
            writter.WriteStartElement(nameof(Author));

            writter.WriteAttributeString(nameof(author.FirstName), author.FirstName);
            writter.WriteAttributeString(nameof(author.SecondName), author.SecondName);

            writter.WriteEndElement();
        }

        private static void WriteLibraryItem(XmlWriter writter, LibraryItem item)
        {
            switch (item.TypeName)
            {
                case Constants.BookTypeName:
                    Library.AddBook(writter, item as Book);
                    break;

                case Constants.NewspaperTypeName:
                    Library.AddNewspaper(writter, item as Newspaper);
                    break;

                case Constants.PatentName:
                    Library.AddPatent(writter, item as Patent);
                    break;

                default:
                    throw new InvalidOperationException($"Unknown item type {item.TypeName}");
            }
        }

        private static void WriteTag(this XmlWriter writter, string name, object value)
        {
            writter.WriteStartElement(name);
            writter.WriteValue(value.ToString());
            writter.WriteEndElement();
        }
    }
}