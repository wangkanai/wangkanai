// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>
///     Provides the APIs for query client access device.
/// </summary>
public interface IUserAgentService
{
	/// <summary>
	///     Get the <see cref="UserAgent" /> of the request client.
	/// </summary>
	UserAgent UserAgent { get; }
}