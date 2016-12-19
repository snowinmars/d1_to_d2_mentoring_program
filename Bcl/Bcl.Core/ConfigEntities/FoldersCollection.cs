using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    [ConfigurationCollection(typeof(FolderElement), AddItemName = "folder")]
    public class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).Path;
        }

        public FolderElement this[int idx]
            => (FolderElement)this.BaseGet(idx);
    }
}
