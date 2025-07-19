// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection.Models;
using Wangkanai.Responsive.Extensions;

using Xunit;

namespace Wangkanai.Responsive.Hosting;

public class HttpContextExtensionsTest
{
	[Fact]
	public void GetDevice_HttpContext_ReturnsExpected()
	{
		var device = Device.Tablet;
		var context = new DefaultHttpContext();
		context.SetDevice(device);

		Assert.Equal(device, context.GetDevice());
		Assert.Equal(device, context.GetDevice());
	}

	[Fact]
	public void GetDevice_Null_ThrowsArgumentNullException()
	{
		Assert.Throws<ArgumentNullException>(() => ((HttpContext)null!).GetDevice());
	}

	[Fact]
	public void SetDevice_InvalidContext_ReturnsNotNull()
	{
		var context = new DefaultHttpContext();
		Assert.Equal("Desktop", context.GetDevice().ToString());
	}
}
