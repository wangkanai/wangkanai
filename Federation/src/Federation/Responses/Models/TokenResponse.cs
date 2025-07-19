// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Responses;

public class TokenResponse : IDPoPResponse
{
	public string IdentityToken { get; set; }
	public string AccessToken { get; set; }
	public string AccessTokenType { get; set; }
	public int AccessTokenLifetime { get; set; }
	public string RefreshToken { get; set; }
	public string Scope { get; set; }
	public string DPoPNonce { get; set; }

	public Dictionary<string, object> Custom { get; set; } = new();
}
