// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Extensions;

namespace Wangkanai.Federation.Hosting;

public class EndpointRouter : IEndpointRouter
{
	private readonly FederationOptions       _options;
	private readonly IEnumerable<Endpoint>   _endpoints;
	private readonly ILogger<EndpointRouter> _logger;

	public EndpointRouter(
		FederationOptions       options,
		IEnumerable<Endpoint>   endpoints,
		ILogger<EndpointRouter> logger)
	{
		_options   = options;
		_endpoints = endpoints;
		_logger    = logger;
	}

	public IEndpointHandler Find(HttpContext context)
	{
		context.ThrowIfNull();

		foreach (var endpoint in _endpoints)
		{
			var path = endpoint.Path;
			if (context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase))
				return GetEndpointHandler(endpoint, context);
		}

		return null;
	}

	private IEndpointHandler GetEndpointHandler(Endpoint endpoint, HttpContext context)
	{
		if (_options.Endpoints.IsEnabled(endpoint))
			if (context.RequestServices.GetService(endpoint.Handler) is IEndpointHandler handler)
				return handler;

		return null;
	}
}