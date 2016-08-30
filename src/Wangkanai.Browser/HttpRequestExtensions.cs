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
    }
}
