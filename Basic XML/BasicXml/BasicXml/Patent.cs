using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml
{
    public class Patent : LibraryItem
    {
        public Patent() : base("Patent")
        {
            this.Authors = new List<Author>();
        }

        public IList<Author> Authors { get; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime FilingDate { get; set; }
        public string Annotation { get; set; }
    }
}
