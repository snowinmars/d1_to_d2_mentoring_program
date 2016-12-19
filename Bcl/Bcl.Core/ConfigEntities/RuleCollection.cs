using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcl.Core.ConfigEntities
{
    [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
    public class RuleCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).MoveTo;
        }

        public RuleElement this[int idx]
            => (RuleElement)this.BaseGet(idx);
    }
}
