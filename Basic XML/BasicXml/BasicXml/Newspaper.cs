using System;

namespace BasicXml
{
    public class Newspaper : LibraryItem
    {
        public Newspaper() : base("Newspaper")
        {
        }

        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public string Issn { get; set; }
        public int Number { get; set; }
        public string PublisherName { get; set; }
        public int Year { get; set; }
    }
}