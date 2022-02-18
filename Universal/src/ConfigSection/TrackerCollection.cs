using System;
using System.Configuration;

namespace Wangkanai.Universal.ConfigSection
{
    class TrackerCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new TrackerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null) throw new ArgumentNullException("element is null");
            return ((TrackerElement)element).Name;
        }
    }
}
