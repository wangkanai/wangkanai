// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Responses;

public class TokenResponse
{
	public string IdentityToken       { get; set; }
	public string AccessToken         { get; set; }
	public string AccessTokenType     { get; set; }
	public int    AccessTokenLifetime { get; set; }
	public string RefreshToken        { get; set; }
	public string Scope               { get; set; }
	public string Nonce               { get; set; }

	public Dictionary<string, object> Custom { get; set; } = new();
}