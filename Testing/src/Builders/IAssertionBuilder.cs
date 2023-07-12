// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

/// <summary>Helper functions for configuring assertion services</summary>
public interface IAssertionBuilder
{
	/// <summary>Gets the <see cref="IServiceCollection"/> services are attached to.</summary>
	/// <value>The <see cref="IServiceCollection"/> services are attached to.</value>
	IServiceCollection Services { get; }
}