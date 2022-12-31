// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Services;

public interface IServerUris
{
	string Origin   { get; set; }
	string BasePath { get; set; }
	string BaseUri  => Origin + BasePath;
}