using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;

namespace BasicXml.UI
{
    class Program
    {
        public static Book GetNewBook()
        {
            return new Book
            {
                Authors = { Author.GetNewOne(), Author.GetNewOne(), Author.GetNewOne() },
                Annotation = string.Concat(Constants.TextGenerator.GetWords(20)),
                CityName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                Isbn = Constants.Isbn10Generator.Generate(),
                Year = Constants.Random.Next(1800, 2017),
                Id = Guid.NewGuid(),
                PageNumber = Constants.Random.Next(10, 1337),
                Title = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
            };
        }

        static void Main(string[] args)
        {
            Library.AddAll(new[] {Program.GetNewBook(), Program.GetNewBook() , Program.GetNewBook() , Program.GetNewBook() , Program.GetNewBook() });

            var collection = Library.GetAll();
        }
    }
}
