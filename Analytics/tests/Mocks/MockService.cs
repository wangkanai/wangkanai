// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Tests.Mocks;

public class MockService
{
	private static IHttpContextAccessor HttpContextAccessor()
	{
		return new HttpContextAccessor { HttpContext = new DefaultHttpContext() };
	}
}
