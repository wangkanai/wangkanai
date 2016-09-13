// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser
{
    public interface IClientService
    {
        HttpContext Context { get; }
        UserAgent UserAgent { get; }
        Device Device { get; }
        // waiting to implement in the future
        //Browser Browser { get; }
        //Platform Platform { get; }
        //Engine Engine { get; }
        //Engine Engine { get; }
    }
}
