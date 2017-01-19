using System;

namespace BasicXml
{
    public abstract class LibraryItem
    {
        protected LibraryItem(string typeName)
        {
            this.TypeName = typeName;
        }

        public Guid Id { get; set; }
        public int PageNumber { get; set; }
        public string Title { get; set; }
        public string TypeName { get; }
    }
}