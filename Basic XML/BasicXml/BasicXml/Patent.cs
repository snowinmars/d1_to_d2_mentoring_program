using System;
using System.Collections.Generic;

namespace BasicXml
{
    public class Patent : LibraryItem
    {
        public Patent() : base("Patent")
        {
            this.Authors = new List<Author>();
        }

        public string Annotation { get; set; }
        public IList<Author> Authors { get; }
        public string Country { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime PublishingDate { get; set; }
        public int RegistrationNumber { get; set; }
    }
}