// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation;

public sealed class AuthenticationOptions
{
	public string?  CookieAuthenticationScheme { get; set; }
	public TimeSpan CookieLifetime             { get; set; } = FederationConstants.DefaultCookieLifetime;
}