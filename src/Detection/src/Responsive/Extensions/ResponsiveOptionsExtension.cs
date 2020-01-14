using System.Diagnostics;

namespace Wangkanai.Responsive
{
    internal static class ResponsiveOptionsExtension
    {
        [DebuggerStepThrough]
        public static bool IsConfigured(this ViewLocationOptions options)
        {
            return options != null;
        }
    }
}
