using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml
{
    public class Newspaper : LibraryItem
    {
        public Newspaper() : base("Newspaper")
        {
        }

        public string CityName { get; set; }
        public string PublisherName { get; set; }
        public int Year { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Issn { get; set; }
    }
}
