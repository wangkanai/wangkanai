// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Wangkanai.Federation;
using Wangkanai.Federation.AspNetIdentity;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Extension methods for setting up ASP.NET Identity in federation services.
/// </summary>
public static class FederationBuilderExtensions
{
	public static IFederationBuilder AddAspNetIdentity<TUser>(this IFederationBuilder builder)
		where TUser : class
	{
		builder.Services.AddTransientDecorator<IUserClaimsPrincipalFactory<TUser>, UserClaimsFactory<TUser>>();

		builder.Services.Configure<IdentityOptions>(options => {
			options.ClaimsIdentity.UserIdClaimType   = JwtClaimTypes.Subject;
			options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.Name;
			options.ClaimsIdentity.RoleClaimType     = JwtClaimTypes.Role;
			options.ClaimsIdentity.EmailClaimType    = JwtClaimTypes.Email;
		});

		builder.Services.ConfigureApplicationCookie(options => {
			options.Cookie.IsEssential = true;
			options.Cookie.SameSite    = SameSiteMode.None;
		});

		builder.Services.ConfigureExternalCookie(options => {
			options.Cookie.IsEssential = true;
			options.Cookie.SameSite    = SameSiteMode.None;
		});

		builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.TwoFactorRememberMeScheme, options => {
			options.Cookie.IsEssential = true;
		});

		builder.Services.Configure<CookieAuthenticationOptions>(IdentityConstants.TwoFactorRememberMeScheme, options => {
			options.Cookie.IsEssential = true;
		});

		builder.Services.AddAuthentication(options => {
			if (options.DefaultAuthenticateScheme == null && options.DefaultScheme == FederationConstants.DefaultCookieAuthenticationScheme)
				options.DefaultScheme = IdentityConstants.ApplicationScheme;
		});


		return builder;
	}
}