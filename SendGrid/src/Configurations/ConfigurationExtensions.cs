// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Configuration;

namespace Wangkanai.SendGrid.Configurations;

public static class ConfigurationExtensions
{
    public static IConfigurationSection GetSendGrid(this IConfiguration configuration)
    {
        return configuration?.GetSection(SendGridConstants.SendGrid);
    }

    public static string GetApiKey(this IConfigurationSection configuration)
    {
        return configuration?[SendGridConstants.ApiKey];
    }

    public static string GetName(this IConfigurationSection configuration)
    {
        return configuration?[SendGridConstants.Name];
    }

    public static string GetEmail(this IConfigurationSection configuration)
    {
        return configuration?[SendGridConstants.Email];
    }
}