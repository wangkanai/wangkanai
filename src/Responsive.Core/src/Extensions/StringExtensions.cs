using System.Diagnostics;

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
