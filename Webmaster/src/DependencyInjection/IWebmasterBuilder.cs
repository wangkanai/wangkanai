// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Webmaster builder interface</summary>
public interface IWebmasterBuilder
{
   /// <summary>Gets the <see cref="IServiceCollection"/> services are attached to.</summary>
   /// <value>The <see cref="IServiceCollection"/> services are attached to.</value>
   IServiceCollection Services { get; }
}