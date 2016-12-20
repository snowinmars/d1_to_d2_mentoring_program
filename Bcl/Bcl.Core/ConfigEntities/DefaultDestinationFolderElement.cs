using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class DefaultDestinationFolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path",
                                DefaultValue = "",
                                IsKey = true,
                                IsRequired = true)]
        public string Value
            => (string)base["path"];
    }
}