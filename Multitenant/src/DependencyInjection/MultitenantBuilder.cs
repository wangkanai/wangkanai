// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai;

namespace Microsoft.Extensions.DependencyInjection;

public class MultitenantBuilder : IMultitenantBuilder
{
    public MultitenantBuilder(IServiceCollection services) 
        => Services = Check.NotNull(services);

    public IServiceCollection Services { get; }
}