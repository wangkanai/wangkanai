// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Hosting.DependencyInjection;
using Wangkanai.Hosting.Services;

namespace Wangkanai.Hosting;

public class CoreBuilderTests
{
   [Fact]
   public void AddMarkerService()
   {
      var services = new ServiceCollection();
      services.AddMarkerService<MarkerService>();
      var provider = services.BuildServiceProvider();
      var marker   = provider.GetService<MarkerService>();
      Assert.NotNull(marker);
   }

   [Fact]
   public void AddMarkerService_VerifyIsRegistered()
   {
      var services = new ServiceCollection();
      services.AddMarkerService<MarkerService>();
      var provider = services.BuildServiceProvider();
      var app      = new ApplicationBuilder(provider);
      app.VerifyMarkerIsRegistered();
      Assert.NotNull(services);
      Assert.NotNull(provider);
      Assert.NotNull(app);
   }

   [Fact]
   public void AddMarkerService_ThrowsInvalidOptionException_IfMarkerServiceIsNotRegistered()
   {
      var services  = new ServiceCollection();
      var provider  = services.BuildServiceProvider();
      var app       = new ApplicationBuilder(provider);
      var exception = Assert.Throws<InvalidOperationException>(() => app.VerifyMarkerIsRegistered());
      Assert.Equal("MarkerService is not added to ConfigureServices(...)", exception.Message);
   }
}