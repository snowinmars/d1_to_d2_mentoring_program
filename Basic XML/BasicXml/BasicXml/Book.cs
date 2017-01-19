using BasicXml.Common;
using System.Collections.Generic;

namespace BasicXml
{
    public class Book : LibraryItem
    {
        public Book() : base(Constants.BookTypeName)
        {
            this.Authors = new List<Author>();
        }

        public string Annotation { get; set; }
        public IList<Author> Authors { get; }
        public string CityName { get; set; }
        public string Isbn { get; set; }
        public int Year { get; set; }
    }
}