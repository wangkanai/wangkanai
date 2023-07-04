// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Responses;

public class TokenRevocationResponse
{
	public bool   Success   { get; set; }
	public string Error     { get; set; }
	public string TokenType { get; set; }
}