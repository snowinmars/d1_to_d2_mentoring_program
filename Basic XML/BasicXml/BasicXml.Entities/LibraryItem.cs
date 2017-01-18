using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml.Entities
{
    public abstract class LibraryItem
    {
        protected LibraryItem(Type type)
        {
            Type = type;
        }

        public string Annotation { get; set; }
        public Guid Id { get; set; }
        public Type Type { get; }
        public int PageNumber { get; set; }

        public string Title { get; set; }

    }
}
