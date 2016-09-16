// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wangkanai.Browser;

namespace Wangkanai.Browser
{
    // concept of extension to HttpRequest
    public static class HttpRequestExtensions
    {
        public static Browser Browser(this HttpRequest request)
        {
            return new Browser();            
        }

        public static Device Device(this HttpRequest request)
        {
            return new Device();
        }

        public static Engine Engine(this HttpRequest request)
        {
            return new Engine();            
        }

        public static Platform Platform(this HttpRequest request)
        {
            return new Platform();
        }
    }
}
