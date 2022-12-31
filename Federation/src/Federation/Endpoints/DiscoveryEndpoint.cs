// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Wangkanai.Federation.Hosting;
using Wangkanai.Federation.Responses;
using Wangkanai.Federation.Services;

namespace Wangkanai.Federation.Endpoints;

public class DiscoveryEndpoint : IEndpointHandler
{
	private readonly FederationOptions          _options;
	private readonly IDiscoveryResponseMaker    _responseMaker;
	private readonly IServerUris                _uris;
	private readonly ILogger<DiscoveryEndpoint> _logger;

	public DiscoveryEndpoint(
		FederationOptions          options,
		IDiscoveryResponseMaker    responseMaker,
		IServerUris                uris,
		ILogger<DiscoveryEndpoint> logger)
	{
		_options       = options;
		_responseMaker = responseMaker;
		_uris          = uris;
		_logger        = logger;
	}


	public async Task<IEndpointResult> ProcessAsync(HttpContext context)
	{
		_logger.LogTrace("Processing discovery request");

		_logger.LogDebug("Start discovery request");

		var baseUri   = _uris.BaseUri;
		var issuerUri = "";

		var response = await _responseMaker.CreateResultAsync(issuerUri, baseUri);

		return new DiscoveryResult(response, _options.Discovery.ResponseCacheMaxAge);
	}
}