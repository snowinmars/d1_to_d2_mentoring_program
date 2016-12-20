using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path",
                                DefaultValue = "",
                                IsKey = true,
                                IsRequired = false)]
        public string Path
            => (string)base["path"];
    }
}