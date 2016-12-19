using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("regex",
                                DefaultValue = "",
                                IsKey = true,
                                IsRequired = false)]
        public string Regex
            => (string)base["regex"];

        [ConfigurationProperty("moveto",
                                DefaultValue = "",
                                IsKey = false,
                                IsRequired = false)]
        public string MoveTo
            => (string)base["moveto"];
    }
}
