// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Http
{
    public static class ResponsiveHttpContextExtensions
    {
        public static ResponsiveExtension Responsive(
            this HttpContext context)
        {
            return new ResponsiveExtension();
        }
    }

    public class ResponsiveExtension
    {
    }
}
