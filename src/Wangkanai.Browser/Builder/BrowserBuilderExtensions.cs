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
        // Concept idea on detecting crawler to browser service
        public static IBrowserBuilder AddCrawler(this IBrowserBuilder builder)
        {
            return builder;
        }

        // Concept idea on add extensive praser to browser service
        public static IBrowserBuilder AddExtensive(this IBrowserBuilder builder)
        {
            return builder;
        }
    }
}
