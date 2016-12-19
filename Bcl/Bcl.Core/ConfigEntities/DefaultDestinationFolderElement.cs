using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    public class DefaultDestinationFolderElement : ConfigurationElement
    {
        [ConfigurationProperty("path",
                                DefaultValue = "",
                                IsKey = true,
                                IsRequired = false)]
        public string Value
            => (string)base["path"];
    }
}
