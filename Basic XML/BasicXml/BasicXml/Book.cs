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

        public IList<Author> Authors { get; }

        public string Annotation { get; set; }

        public string CityName { get; set; }

        public int Year { get; set; }

        public string Isbn { get; set; }
    }
}
