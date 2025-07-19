// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
