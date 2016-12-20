using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("moveto",
                                DefaultValue = "",
                                IsKey = false,
                                IsRequired = false)]
        public string MoveTo
            => (string)base["moveto"];

        [ConfigurationProperty("regex",
                                        DefaultValue = "",
                                IsKey = true,
                                IsRequired = false)]
        public string Regex
            => (string)base["regex"];
    }
}