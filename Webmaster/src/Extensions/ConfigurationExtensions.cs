// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Configuration;

namespace Wangkanai.Webmaster;

public static class ConfigurationExtensions
{
    public static string GetAzureAd(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.AzureAd))?[name];

    public static string GetSendgrid(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.SendGrid))?[name];

    public static string GetRecaptcha(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.Recaptcha))?[name];

    public static string GetGoogle(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.Google))?[name];

    public static string GetFacebook(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.Facebook))?[name];

    public static string GetLinkedIn(this IConfiguration configuration, string name)
        => configuration?.GetSection(nameof(AppSetting.LinkedIn))?[name];
}