using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.Entities
{
    public class Newspaper: LibraryItem
    {
        public Newspaper() : base(typeof(Newspaper))
        {
        }

        public string CityName { get; set; }
        public string PublisherName { get; set; }
        public int Year { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}
