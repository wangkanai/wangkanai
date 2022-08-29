// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.SendGrid.DependencyInjection;
using Wangkanai.SendGrid.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class SendGridCollectionExtensions
{
    public static ISendGridBuilder AddSendGrid(this IServiceCollection services)
        => services.AddSendGridBuilder()
                   .AddRequiredPlatformServices()
                   .AddCoreServices();

    public static ISendGridBuilder AddSendGrid(this IServiceCollection services, Action<SendGridOptions> options)
        => services.Configure(options)
                   .AddSendGrid();

    private static ISendGridBuilder AddSendGridBuilder(this IServiceCollection services)
        => new SendGridBuilder(services);
}