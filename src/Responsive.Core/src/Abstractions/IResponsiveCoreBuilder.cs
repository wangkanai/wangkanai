// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Responsive
{
    public interface IResponsiveCoreBuilder
    {
        IServiceCollection Services { get; }
    }
}
