using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicXml.Common;

namespace BasicXml.Generators
{
    public static class EntityGenerator
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

        public static Newspaper GetNewNewspaper()
        {
            return new Newspaper
            {
                CityName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                Issn = Constants.IssnGenerator.Generate(),
                Year = Constants.Random.Next(1800, 2017),
                PageNumber = Constants.Random.Next(10, 1337),
                Id = Guid.NewGuid(),
                Title = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                Date = TypeExtensions.RandomDateTime(),
                Number = Constants.Random.Next(10, 1337),
                PublisherName = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
            };
        }

        public static Patent GetNewPatent()
        {
            return new Patent
            {
                PageNumber = Constants.Random.Next(10, 1337),
                Id = Guid.NewGuid(),
                Title = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                Authors = { Author.GetNewOne(), Author.GetNewOne(), Author.GetNewOne() },
                Annotation = string.Concat(Constants.TextGenerator.GetWords(20)),
                Country = Constants.TextGenerator.GetNewWord(3, 12, isFirstLerretUp: true),
                FilingDate = TypeExtensions.RandomDateTime(),
                PublishingDate = TypeExtensions.RandomDateTime(),
                RegistrationNumber = Constants.Random.Next(10, 1337),
            };
        }
    }
}
