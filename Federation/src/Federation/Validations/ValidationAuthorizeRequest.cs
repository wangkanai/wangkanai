// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Validations;

public class ValidationAuthorizeRequest : ValidationRequest
{
	public string RedirectUri { get; set; }
	public string State       { get; set; }
}