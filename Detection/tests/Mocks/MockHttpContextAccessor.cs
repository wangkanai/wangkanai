// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Mocks;

public class MockHttpContextAccessor : IHttpContextAccessor
{
	public HttpContext? HttpContext { get; set; }
}
