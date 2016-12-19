using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    public class IsVerboseElement : ConfigurationElement
    {
        [ConfigurationProperty("value",
                                DefaultValue = true,
                                IsKey = true,
                                IsRequired = false)]
        public bool Value
            => (bool)base["value"];
    }
}
