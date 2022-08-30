// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using Wangkanai.SendGrid.Options;
using Wangkanai.SendGrid.Services;

namespace Wangkanai.SendGrid.DependencyInjection;

public static class SendGridBuilderExtensions
{
    public static ISendGridBuilder AddRequiredPlatformServices(this ISendGridBuilder builder)
    {
        Check.NotNull(builder);

        builder.Services.AddOptions();
        builder.Services.TryAddSingleton(
            serviceProvider => serviceProvider.GetRequiredService<IOptions<SendGridOptions>>()
                                              .Value
        );

        return builder;
    }

    public static ISendGridBuilder AddCoreServices(this ISendGridBuilder builder)
    {
        Check.NotNull(builder);

        builder.Services.AddScoped<ISendGridService, SendGridService>();
        //builder.Services.AddScoped<>();

        return builder;
    }
}