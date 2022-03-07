// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AnalyticsCollectionExtensions
    {
        public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services)
            => services.AddAnalyticsBuilder()
                       .AddRequiredPlatformServices()
                       .AddCoreServices()
                       .AddMarkerService();

        public static IAnalyticsBuilder AddAnalytics(this IServiceCollection services, Action<AnalyticsOptions> setAction)
            => services.Configure(setAction)
                       .AddAnalytics();

        internal static IAnalyticsBuilder AddAnalyticsBuilder(this IServiceCollection services)
            => new AnalyticsBuilder(services);
    }
}