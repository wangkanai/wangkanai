// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Mocks;

using Xunit;

namespace Wangkanai.Responsive.DependencyInjection;

public class ResponsiveBuilderExtensionsTest
{
    [Fact]
    public void AddResponsive_Options_Builder_Service()
    {
        var service = new ServiceCollection();
        var builder = service.AddResponsive(
            options => { options.DefaultTablet = Device.Desktop; });

        Assert.Same(service, builder.Services);
    }

    [Fact]
    public async void AddResponsive_Options_Disable_True()
    {
        using var server = MockServer.Server(options => { options.Disable = true; });

        var client   = server.CreateClient();
        var request  = MockClient.CreateRequest(Device.Mobile);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Assert.Contains("desktop", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }


    [Fact]
    public async void AddResponsive_Options_Disable_False()
    {
        using var server = MockServer.Server(options => { options.Disable = false; });

        var client   = server.CreateClient();
        var request  = MockClient.CreateRequest(Device.Mobile);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Assert.Contains("mobile", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void AddResponsive_Options_Disable_IncludeWebApi()
    {
        var builder = MockServer.WebHostBuilder(options =>
        {
            options.Disable       = true;
            options.IncludeWebApi = true;
        });

        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            using var server = new TestServer(builder);
        });
        Assert.Equal("IncludeWebApi is not needed if already Disable", exception.Message);
    }

    [Theory]
    [InlineData(Device.Mobile, "desktop", "/api/dog")]
    [InlineData(Device.Mobile, "mobile", "/api/dog")]
    public async void AddResponsive_WebApi_Exclude_Api(Device device, string agent, string path)
    {
        using var server = MockServer.Server(options =>
        {
            options.DefaultMobile  = device;
            options.DefaultTablet  = device;
            options.DefaultDesktop = device;
            options.IncludeWebApi  = true;
        });

        var request = server.CreateRequest(path);
        request.AddHeader("User-Agent", agent);
        //var request  = MockClient.CreateRequest(agent, path);
        var response = await request.GetAsync();
        response.EnsureSuccessStatusCode();
        Assert.Contains("mobile", await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(Device.Desktop, "mobile", "")]
    [InlineData(Device.Desktop, "desktop", "")]
    [InlineData(Device.Desktop, "mobile", "/api/dog")]
    [InlineData(Device.Desktop, "desktop", "/api/dog")]
    [InlineData(Device.Mobile, "desktop", "")]
    [InlineData(Device.Mobile, "mobile", "")]
    public async void AddResponsive_WebApi_Exclude_NonApi(Device device, string agent, string path)
    {
        using var server = MockServer.Server(options =>
        {
            options.DefaultMobile  = device;
            options.DefaultTablet  = device;
            options.DefaultDesktop = device;
            options.IncludeWebApi  = false;
        });
        await server.Host.StartAsync();

        var client   = server.CreateClient();
        var request  = MockClient.CreateRequest(agent, path);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Assert.Contains(device.ToString(), await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(Device.Desktop, "mobile", "")]
    [InlineData(Device.Desktop, "mobile", "/api/dog")]
    [InlineData(Device.Desktop, "desktop", "")]
    [InlineData(Device.Desktop, "desktop", "/api/dog")]
    [InlineData(Device.Mobile, "desktop", "")]
    [InlineData(Device.Mobile, "desktop", "/api/dog")]
    [InlineData(Device.Mobile, "mobile", "")]
    [InlineData(Device.Mobile, "mobile", "/api/dog")]
    public async void AddResponsive_WebApi_Include_Api(Device device, string agent, string path)
    {
        using var server = MockServer.Server(options =>
        {
            options.DefaultMobile  = device;
            options.DefaultTablet  = device;
            options.DefaultDesktop = device;
            options.IncludeWebApi  = true;
        });

        var client   = server.CreateClient();
        var request  = MockClient.CreateRequest(agent, path);
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Assert.Contains(device.ToString(), await response.Content.ReadAsStringAsync(), StringComparison.OrdinalIgnoreCase);
    }
}