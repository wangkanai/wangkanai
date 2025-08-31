// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Testing;
using Wangkanai.Testing.Builders;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>Contains extensions method to <see cref="IServiceCollection"/> for configuring client services</summary>
public static class AssertionCollectionExtensions
{
   /// <summary>Add Assertion service to the services container</summary>
   /// <param name="services">The services available in the application</param>
   /// <returns>An <see cref="IAssertionBuilder"/> so that additional calls can be chained</returns>
   public static IAssertionBuilder AddAssertion(this IServiceCollection services)
      => services.AddAssertionBuilder()
                 .AddRequiredService()
                 .AddLifetimeService()
                 .AddMockService()
                 .AddMarkerService();

   /// <summary>Add Assertion service to the service container</summary>
   /// <param name="services">The services available in the application</param>
   /// <param name="configure">An <see cref="Action{AssertionOptions}"/> to configure the provided <see cref="ResponsiveOptions"/></param>
   /// <returns>An <see cref="IAssertionBuilder"/> so that additional calls can be chained</returns>
   public static IAssertionBuilder AddAssertion(this IServiceCollection services, Action<AssertionOptions> configure)
      => services.Configure(configure)
                 .AddAssertionBuilder();

   // For internal unit tests
   internal static IAssertionBuilder AddAssertionBuilder(this IServiceCollection services)
      => new AssertionBuilder(services);
}