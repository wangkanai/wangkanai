// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Validations;

namespace Wangkanai.Federation.Responses;

public class AuthorizeResponse
{
	public ValidationAuthorizeRequest Request { get; set; }
	public string RedirectUri => Request?.RedirectUri;
}