// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Webmaster.Builders;

/// <summary>Helper functions for configuring Webmaster services.</summary>
public sealed class WebmasterBuilder : IWebmasterBuilder
{
   /// <summary>Creates a new instance of <see cref="WebmasterBuilder"/>.</summary>
   /// <param name="services">The <see cref="IServiceCollection"/> to attach to.</param>
   public WebmasterBuilder(IServiceCollection services)
      => Services = services.ThrowIfNull();

   /// <summary>Gets the <see cref="IServiceCollection"/> services are attached to.</summary>
   /// <value>The <see cref="IServiceCollection"/> services are attached to.</value>
   public IServiceCollection Services { get; }
}