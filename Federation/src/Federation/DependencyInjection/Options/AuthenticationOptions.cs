// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Federation;

public sealed class AuthenticationOptions
{
	public string?      CookieAuthenticationScheme     { get; set; }
	public TimeSpan     CookieLifetime                 { get; set; } = FederationConstants.DefaultCookieLifetime;
	public string       SessionCookieName         { get; set; } = FederationConstants.DefaultCheckSessionCookieName;
	public string?      SessionCookieDomain       { get; set; }
	public SameSiteMode SessionCookieSameSiteMode { get; set; } = SameSiteMode.None;
}