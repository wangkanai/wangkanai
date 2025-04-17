// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>
///     Provides the APIs for query client <see cref="Browser" />.
/// </summary>
public interface IBrowserService
{
	/// <summary>
	///     Gets the <see cref="Browser" /> name of the request client.
	/// </summary>
	public Browser Name { get; }

	/// <summary>
	///     Gets the <see cref="Version" /> of the request client.
	/// </summary>
	public Version Version { get; }
}