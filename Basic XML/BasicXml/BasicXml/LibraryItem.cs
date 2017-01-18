using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicXml
{
    public abstract class LibraryItem
    {
        protected LibraryItem(string typeName)
        {
            this.TypeName = typeName;
        }

        public string TypeName { get;  }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public int PageNumber { get; set; }
    }
}
