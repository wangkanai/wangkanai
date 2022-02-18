using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    internal class EnhancedLink : Require
    {
        private EnhancedOption option { get; set; }
        public EnhancedLink(Configuration config)
        {
            if (string.IsNullOrEmpty(config.CookieName))           
                option = new EnhancedOption(config);        
        }

        public override string ToString()
        {
            return (string.IsNullOrEmpty(option.CookieName)) ?
                "ga('require', 'linkid', 'linkid.js');" :
                string.Format("ga('require', 'linkid', 'linkid.js', {0});", option.ToString());
        }
    }
}
