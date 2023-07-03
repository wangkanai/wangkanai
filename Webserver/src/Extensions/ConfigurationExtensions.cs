// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Configuration;

using Wangkanai.Webserver;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigurationExtensions
{
	public static string GetAzureAd(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.AzureAd))?[name];

	public static string GetSendgrid(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.SendGrid))?[name];

	public static string GetRecaptcha(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.Recaptcha))?[name];

	public static string GetGoogle(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.Google))?[name];

	public static string GetFacebook(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.Facebook))?[name];

	public static string GetLinkedIn(this IConfiguration configuration, string name)
		=> configuration?.GetSection(nameof(AppSettings.LinkedIn))?[name];
}