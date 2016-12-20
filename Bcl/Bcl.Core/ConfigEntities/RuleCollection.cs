using System.Configuration;

namespace Bcl.Core.ConfigEntities
{
    [ConfigurationCollection(typeof(RuleElement), AddItemName = "rule")]
    public class RuleCollection : ConfigurationElementCollection
    {
        public RuleElement this[int idx]
            => (RuleElement)this.BaseGet(idx);

        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleElement)element).MoveTo;
        }
    }
}