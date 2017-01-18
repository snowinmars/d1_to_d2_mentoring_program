using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BasicXml.Common;

namespace BasicXml.Entities
{
    public class Book:LibraryItem
    {
        public Book() : base(typeof(Book))
        {
            this.Authors = new List<Author>();
        }

        public IList<Author> Authors { get; }

        public string CityName { get; set; }

        public string PublisherName { get; set; }
        public int Year { get; set; }
        public string ISBN { get; set; }

        public void WriteAsXmlTo(XmlWriter writer)
        {
            writer.WriteStartElement("Book");

            writer.WriteStartElement("Type");
            writer.WriteValue(this.Type.Name);
            writer.WriteEndElement();

            writer.WriteStartElement("Id");
            writer.WriteValue(this.Id.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Annotation");
            writer.WriteValue(this.Annotation);
            writer.WriteEndElement();

            writer.WriteStartElement("PageNumber");
            writer.WriteValue(this.PageNumber);
            writer.WriteEndElement();

            writer.WriteStartElement("Title");
            writer.WriteValue(this.Title);
            writer.WriteEndElement();

            writer.WriteStartElement("CityName");
            writer.WriteValue(this.CityName);
            writer.WriteEndElement();

            writer.WriteStartElement("ISBN");
            writer.WriteValue(this.ISBN);
            writer.WriteEndElement();

            writer.WriteStartElement("PublisherName");
            writer.WriteValue(this.PublisherName);
            writer.WriteEndElement();

            writer.WriteStartElement("Year");
            writer.WriteValue(this.Year);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        public static Book GetNewOne()
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Annotation = string.Concat(Constant.TextGenerator.GetWords(10)),
                Authors =
                {
                    new Author { FirstName = Constant.TextGenerator.GetNewWord(4,7), LastName = Constant.TextGenerator.GetNewWord(4, 7) },
                    new Author { FirstName = Constant.TextGenerator.GetNewWord(4,7), LastName = Constant.TextGenerator.GetNewWord(4, 7) },
                    new Author { FirstName = Constant.TextGenerator.GetNewWord(4,7), LastName = Constant.TextGenerator.GetNewWord(4, 7) },
                },
                CityName = Constant.TextGenerator.GetNewWord(4, 7),
                ISBN = Constant.Isbn10Generator.Generate(),
                PageNumber = Constant.Random.Next(),
                PublisherName = Constant.TextGenerator.GetNewWord(4, 7),
                Title = Constant.TextGenerator.GetNewWord(4, 7),
                Year = Constant.Random.Next(),
            };
        }
    }
}
