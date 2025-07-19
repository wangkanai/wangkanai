// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Identity;

namespace Wangkanai.Federation.Responses;

public class TokenErrorResponse : IDPoPResponse
{
	public string Error { get; set; } = OidcConstants.TokenErrors.InvalidRequest;
	public string Description { get; set; }
	public string DPoPNonce { get; set; }

	public Dictionary<string, object> Custom { get; set; } = new();
}
