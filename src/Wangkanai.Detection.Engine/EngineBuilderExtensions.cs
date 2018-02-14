// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection.Extensions;
using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{    
    public static class EngineBuilderExtensions
    {
        // Concept idea on adding engine detection to client service
        public static IDetectionBuilder AddEngine(this IDetectionBuilder builder)
        {
            builder.Services.TryAddTransient<IEngineResolver, EngineResolver>();

            return builder;
        }
    }
}
