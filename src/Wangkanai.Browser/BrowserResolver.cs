// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    internal class BrowserResolver
    {
        private readonly HttpRequest _request;

        public BrowserResolver(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _request = context.Request;                        
        }

        internal BrowserInfo BrowserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
