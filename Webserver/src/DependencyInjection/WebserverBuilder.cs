// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Microsoft.Extensions.DependencyInjection;

public sealed class WebserverBuilder : IWebserverBuilder
{
   public WebserverBuilder(IServiceCollection services)
      => Services = services.ThrowIfNull();

   public IServiceCollection Services { get; }
}