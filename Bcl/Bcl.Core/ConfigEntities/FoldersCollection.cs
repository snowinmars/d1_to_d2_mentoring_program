using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    [ConfigurationCollection(typeof(FolderElement), AddItemName = "folder")]
    public class FoldersCollection : ConfigurationElementCollection
    {
        public FolderElement this[int idx]
            => (FolderElement)this.BaseGet(idx);

        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)element).Path;
        }
    }
}