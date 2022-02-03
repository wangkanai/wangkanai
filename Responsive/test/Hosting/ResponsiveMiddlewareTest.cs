// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Mocks;
using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Hosting;

public class ResponsiveMiddlewareTest
{
    private static Task Next(HttpContext d)
        => Task.Factory.StartNew(() => d);

    [Fact]
    public void Ctor_RequestDelegate_Null_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(
            () => new ResponsiveMiddleware(null!)
        );
    }

    [Fact]
    public async void Invoke_HttpContext_Null_ResponsiveService_Null_ThrowsArgumentNullException()
    {
        var middleware = new ResponsiveMiddleware(Next);

        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await middleware.InvokeAsync(null!, null!)
        );
    }

    [Fact]
    public async void Invoke_HttpContext_ResponsiveService_Null_ThrowsNullReferenceException()
    {
        var service    = MockService.HttpContextService(null!);
        var middleware = new ResponsiveMiddleware(Next);

        await Assert.ThrowsAsync<NullReferenceException>(
            async () => await middleware.InvokeAsync(service.Context, null!)
        );
    }

    [Fact]
    public async void Invoke_HttpContext_ResponsiveService_Success()
    {
        using var server   = MockServer.ServerResponsive();
        var       request  = MockClient.CreateRequest(Device.Desktop);
        var       client   = server.CreateClient();
        var       response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Assert.Contains(
            "desktop",
            await response.Content.ReadAsStringAsync(),
            StringComparison.OrdinalIgnoreCase);
    }
}