// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Responsive.Mocks;

public class MockHttpContextAccessor : IHttpContextAccessor
{
	public HttpContext? HttpContext { get; set; } = new DefaultHttpContext();
}
