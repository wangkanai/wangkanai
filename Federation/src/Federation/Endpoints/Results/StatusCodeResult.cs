// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Net;

using Microsoft.AspNetCore.Http;

using Wangkanai.Federation.Hosting;

namespace Wangkanai.Federation.Endpoints.Results;

/// <summary>
/// Result for a RAW Http status code
/// </summary>
public sealed class StatusCodeResult : IEndpointResult
{
	public int StatusCode { get; }

	public StatusCodeResult(HttpStatusCode statusCode)
		=> StatusCode = (int)statusCode;

	public StatusCodeResult(int statusCode)
		=> StatusCode = statusCode;


	public Task ExecuteAsync(HttpContext context)
	{
		context.Response.StatusCode = StatusCode;

		return Task.CompletedTask;
	}
}
