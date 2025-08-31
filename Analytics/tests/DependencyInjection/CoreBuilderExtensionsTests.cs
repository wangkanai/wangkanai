// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using Wangkanai.Analytics.Services;
using Wangkanai.Testing;

namespace Microsoft.Extensions.DependencyInjection;

public class CoreBuilderExtensionsTests
{
   [Fact]
   public void AddMarkerServices_ReturnsExpected()
   {
      var services    = new ServiceCollection();
      var builder     = services.AddAnalyticsBuilder().AddMarkerService();
      var descriptors = new List<ServiceDescriptor>();
      descriptors.Add(new(typeof(AnalyticsMarkerService), typeof(AnalyticsMarkerService), ServiceLifetime.Singleton));

      Assert.NotNull(builder);
      Assert.NotNull(builder.Services);
      descriptors.AssertServices(builder.Services);
   }

   [Fact]
   public void AddRequiredPlatformServices_ReturnsExpected()
   {
      var services    = new ServiceCollection();
      var builder     = services.AddAnalyticsBuilder().AddRequiredServices();
      var descriptors = new List<ServiceDescriptor>();
      descriptors.Add(new(typeof(IHttpContextAccessor), typeof(HttpContextAccessor), ServiceLifetime.Singleton));
      descriptors.Add(new(typeof(IOptions<>), typeof(AnalyticsOptions), ServiceLifetime.Singleton));
      descriptors.Add(new(typeof(IOptionsSnapshot<>), typeof(AnalyticsOptions), ServiceLifetime.Scoped));
      descriptors.Add(new(typeof(IOptionsMonitor<>), typeof(AnalyticsOptions), ServiceLifetime.Singleton));
      descriptors.Add(new(typeof(IOptionsFactory<>), typeof(AnalyticsOptions), ServiceLifetime.Transient));
      descriptors.Add(new(typeof(IOptionsMonitorCache<>), typeof(AnalyticsOptions), ServiceLifetime.Singleton));
      descriptors.Add(new(typeof(AnalyticsOptions), typeof(AnalyticsOptions), ServiceLifetime.Singleton));

      Assert.NotNull(builder);
      Assert.NotNull(builder.Services);
      descriptors.AssertServices(builder.Services);
   }

   [Fact]
   public void AddCoreServices_ReturnsExpected()
   {
      var services    = new ServiceCollection();
      var builder     = services.AddAnalyticsBuilder().AddCoreServices();
      var descriptors = new List<ServiceDescriptor>();
      descriptors.Add(new(typeof(IAnalyticsService), typeof(AnalyticsService), ServiceLifetime.Scoped));

      Assert.NotNull(builder);
      Assert.NotNull(builder.Services);
      descriptors.AssertServices(builder.Services);
   }

   [Fact]
   public void AddAnalytics_Null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddAnalytics());

   [Fact]
   public void AddAnalyticsBuilder_Null_ArgumentNullException() => Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null!).AddAnalyticsBuilder());
}