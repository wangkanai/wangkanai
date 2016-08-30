using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.Browser.Abstractions;

namespace Wangkanai.Browser
{
    public class BrowserBuilder : IBrowserBuilder
    {
        public BrowserBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; private set; }
    }
}
