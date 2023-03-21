// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Endpoints;

public class AuthorizedEndpoint: IEndpointHandler
{
	public async Task<IEndpointResult> ProcessAsync(HttpContext context)
	{
		throw new NotImplementedException();
	}
}