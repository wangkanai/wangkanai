// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// An interface for configuring markdown services.
/// </summary>
public interface IMarkdownBuilder
{
	/// <summary>
	///     Gets the <see cref="IServiceCollection" /> services are attached to.
	/// </summary>
	IServiceCollection Services { get; }

	/// <summary>
	/// Gets the <see cref="ApplicationPartManager"/> where <see cref="ApplicationPart"/>s are configured.
	/// </summary>
	// ApplicationPartManager PartManager { get; }
}
