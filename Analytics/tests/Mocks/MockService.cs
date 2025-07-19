// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Tests.Mocks;

public class MockService
{
	private static IHttpContextAccessor HttpContextAccessor()
	{
		return new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
	}
}
