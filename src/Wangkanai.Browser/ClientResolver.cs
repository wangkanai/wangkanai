// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    internal class ClientResolver
    {
        internal ClientInfo ClientInfo => new ClientInfo();

        private readonly HttpRequest _request;

        public ClientResolver(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _request = context.Request;
        }        
    }
}
