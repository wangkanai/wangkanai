// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextExtensions
    {
        public static string Platform(this HttpContext context)
        {
            return "Platform working!";
        }
    }
}
