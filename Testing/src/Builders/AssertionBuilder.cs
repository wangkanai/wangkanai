// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing.Builders;

/// <summary>Helper function for configuring assertion services</summary>
public class AssertionBuilder : IAssertionBuilder
{
	/// <summary>Create a new instance of <see cref="IAssertionBuilder" /> </summary>
	/// <param name="services">The <see cref="IServiceCollection" /> to attach to</param>
	public AssertionBuilder(IServiceCollection services)
		=> Services = services.ThrowIfNull();

	/// <summary>Gets the <see cref="IServiceCollection" /> services are attached to</summary>
	/// <value>The <see cref="IServiceCollection" /> services are attached to</value>
	public IServiceCollection Services { get; }
}
