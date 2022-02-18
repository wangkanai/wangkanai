using Wangkanai.Universal.Options;

namespace Wangkanai.Universal.Models
{
    internal class EnhancedLink : Require
    {
        private EnhancedOption option { get; set; }
        
        public override string ToString()
        {
            return (string.IsNullOrEmpty(option.CookieName)) ?
                "ga('require', 'linkid', 'linkid.js');" :
                string.Format("ga('require', 'linkid', 'linkid.js', {0});", option);
        }
    }
}
