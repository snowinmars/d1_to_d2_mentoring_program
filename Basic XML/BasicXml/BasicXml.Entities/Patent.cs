using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.Entities
{
    public class Patent: LibraryItem
    {
        public Patent() : base(typeof(Patent))
        {
            this.Inventors = new List<Author>();
        }

        public IEnumerable<Author> Inventors { get; }

        public string CountryName { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
