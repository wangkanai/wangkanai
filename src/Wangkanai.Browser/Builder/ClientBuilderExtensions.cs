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
    [Obsolete]
    public static class ClientBuilderExtensions
    {
        // Concept idea on detecting crawler to client service
        public static IClientBuilder AddPlatform(this IClientBuilder builder)
        {
            return builder;
        }

        // Concept idea on add extensive praser to client service
        public static IClientBuilder AddEngine(this IClientBuilder builder)
        {
            return builder;
        }
    }
}
