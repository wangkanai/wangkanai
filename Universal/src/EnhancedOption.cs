using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wangkanai.Universal
{
    internal class EnhancedOption : FieldOption
    {
        public string CookieName { get; set; }
        public int Duration { get; set; }
        public int Levels { get; set; }

        public EnhancedOption(Configuration config)
        {
            CookieName = config.EnhancedCookieName;
            Duration = config.EnhancedDuration;
            Levels = config.EnhancedLevels;
        }
    }
}
