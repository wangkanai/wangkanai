// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Responsive
{
    public interface IResponsiveOptionsBuilder
    {
        IApplicationBuilder ApplicationBuilder { get; }
        IServiceProvider ServiceProvider { get; }
        //IViewLocation DefaultHandler { get; set; }
    }
}
