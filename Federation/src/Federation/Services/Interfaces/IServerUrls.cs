// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Services;

/// <summary>
/// Configures the per-request URIs and paths into the current server
/// </summary>
public interface IServerUrls
{
	string Origin { get; set; }
	string BasePath { get; set; }
	string BaseUri => Origin + BasePath;
}
