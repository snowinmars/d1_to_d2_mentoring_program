using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    public class MustAddNumberElement : ConfigurationElement
    {
        [ConfigurationProperty("value",
                                DefaultValue = false,
                                IsKey = true,
                                IsRequired = true)]
        public bool Value
            => (bool)base["value"];
    }
}
