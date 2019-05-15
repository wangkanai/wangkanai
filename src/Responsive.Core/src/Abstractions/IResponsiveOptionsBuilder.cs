using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Responsive
{
    public interface IResponsiveOptionsBuilder
    {
        IApplicationBuilder ApplicationBuilder { get; }
        IServiceProvider ServiceProvider { get; }
    }
}
