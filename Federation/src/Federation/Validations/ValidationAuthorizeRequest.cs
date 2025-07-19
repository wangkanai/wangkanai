// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Validations;

public class ValidationAuthorizeRequest : ValidationRequest
{
	public string RedirectUri { get; set; }
	public string State { get; set; }
}
