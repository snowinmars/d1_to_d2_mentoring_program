using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
