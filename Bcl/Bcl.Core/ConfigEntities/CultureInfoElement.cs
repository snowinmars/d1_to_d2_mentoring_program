using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class CultureInfoElement : ConfigurationElement
    {
        [ConfigurationProperty("value",
                                DefaultValue = "en-US",
                                IsKey = true,
                                IsRequired = true)]
        public string Value
            => (string)base["value"];
    }
}