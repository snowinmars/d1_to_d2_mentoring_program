using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class IsVerboseElement : ConfigurationElement
    {
        [ConfigurationProperty("value",
                                DefaultValue = true,
                                IsKey = true,
                                IsRequired = true)]
        public bool Value
            => (bool)base["value"];
    }
}