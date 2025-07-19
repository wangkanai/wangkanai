// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing.Builders;

/// <summary>Helper functions for configuring assertion services</summary>
public interface IAssertionBuilder
{
	/// <summary>Gets the <see cref="IServiceCollection" /> services are attached to.</summary>
	/// <value>The <see cref="IServiceCollection" /> services are attached to.</value>
	IServiceCollection Services { get; }
}
