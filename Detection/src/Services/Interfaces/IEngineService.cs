// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>
///     Provides the APIs for query client browser rendering engine.
/// </summary>
public interface IEngineService
{
	/// <summary>
	///     Get the <see cref="Engine" /> of the request client.
	/// </summary>
	public Engine Name { get; }
}