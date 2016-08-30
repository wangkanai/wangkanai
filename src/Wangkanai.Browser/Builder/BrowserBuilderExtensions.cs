using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Browser.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserBuilderExtensions
    {
        public static IBrowserBuilder AddCrawler(this IBrowserBuilder builder)
        {
            return builder;
        }
    }
}
