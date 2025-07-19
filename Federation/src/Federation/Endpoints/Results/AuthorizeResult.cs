// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Endpoints.Results;

public sealed class AuthorizeResult : IEndpointResult
{
	public AuthorizeResult() { }

	public async Task ExecuteAsync(HttpContext context)
	{
		throw new NotImplementedException();
	}
}
