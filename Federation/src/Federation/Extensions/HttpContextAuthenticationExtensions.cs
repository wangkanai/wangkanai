// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Federation.Extensions;

public static class HttpContextAuthenticationExtensions
{
	internal static async Task<string> GetCookieAuthenticationSchemeAsync(this HttpContext context)
	{
		var options = context.RequestServices.GetRequiredService<FederationOptions>();
		if (options.Authentication.CookieAuthenticationScheme != null)
			return options.Authentication.CookieAuthenticationScheme;

		var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider>();
		var scheme  = await schemes.GetDefaultAuthenticateSchemeAsync();
		scheme.ThrowIfNull<InvalidOperationException>("No DefaultAuthenticationScheme found or no CookieAuthenticationScheme configured on FederationOptions");
		return scheme.Name;
	}
}