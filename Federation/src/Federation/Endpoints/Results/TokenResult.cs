// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Endpoints.Results;

public sealed class TokenResult : IEndpointResult
{
	public Task ExecuteAsync(HttpContext context)
	{
		throw new NotImplementedException();
	}
}
