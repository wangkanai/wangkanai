// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
