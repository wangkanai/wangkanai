// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Browser
{    
    public interface IClientBuilder
    {
        IServiceCollection Services { get; }
    }
}
