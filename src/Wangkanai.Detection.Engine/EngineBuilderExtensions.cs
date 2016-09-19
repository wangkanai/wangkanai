// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection;
using Wangkanai.Detection.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{    
    public static class EngineBuilderExtensions
    {
        // Concept idea on adding engine detection to client service
        public static IDetectionBuilder AddEngine(this IDetectionBuilder builder)
        {
            return builder;
        }
    }
}
