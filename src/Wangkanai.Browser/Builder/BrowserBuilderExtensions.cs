// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wangkanai.Browser;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class BrowserBuilderExtensions
    {
        // Concept idea on detecting crawler to browser service
        public static IBrowserBuilder AddPlatform(this IBrowserBuilder builder)
        {
            return builder;
        }

        // Concept idea on add extensive praser to browser service
        public static IBrowserBuilder AddEngine(this IBrowserBuilder builder)
        {
            return builder;
        }
    }
}
