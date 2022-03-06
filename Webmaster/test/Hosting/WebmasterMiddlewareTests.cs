// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace Wangkanai.Webmaster.Tests.Hosting;

public class WebmasterMiddlewareTests
{
    private static Task Next(HttpContext context)
        => Task.Factory.StartNew(() => context);

    [Fact]
    public void If_Null_Throw_Exception()
    {
        Assert.Throws<ArgumentNullException>(() => new WebmasterMiddleware(null));
    }

    [Fact]
    public async void If_Null_Invoke_Throw_Exception()
    {
        var middleware = new WebmasterMiddleware(Next);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await middleware.InvokeAsync(null));
    }
}