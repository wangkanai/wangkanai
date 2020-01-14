using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wangkanai.Responsive
{
    internal static class StringExtensions
    {
        [DebuggerStepThrough]
        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
