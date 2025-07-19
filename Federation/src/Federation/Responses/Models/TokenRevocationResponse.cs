// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Responses;

public class TokenRevocationResponse
{
	public bool Success { get; set; }
	public string Error { get; set; }
	public string TokenType { get; set; }
}
