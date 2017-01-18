using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;

namespace BasicXml
{
    public class Book : LibraryItem
    {
        public Book() : base(Constants.BookTypeName)
        {
            this.Authors = new List<Author>();
        }

        public static Book GetNewOne()
        {
            return new Book
            {
                Authors = {Author.GetNewOne(), Author.GetNewOne(), Author.GetNewOne()},
                Annotation = string.Concat(Constants.TextGenerator.GetWords(20)),
                CityName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                Isbn = Constants.Isbn10Generator.Generate(),
                Year = Constants.Random.Next(1800, 2017),
                Id = Guid.NewGuid(),
                PageNumber = Constants.Random.Next(10, 1337),
                Title = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
            };
        }

        public IList<Author> Authors { get; }

        public string Annotation { get; set; }

        public string CityName { get; set; }

        public int Year { get; set; }

        public string Isbn { get; set; }
    }
}
