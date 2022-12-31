// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Wangkanai.Federation.Hosting;
using Wangkanai.Federation.Responses;
using Wangkanai.Federation.Results;
using Wangkanai.Federation.Services;

namespace Wangkanai.Federation.Endpoints;

public class DiscoveryEndpoint : IEndpointHandler
{
	private readonly FederationOptions          _options;
	private readonly IIssuerNameService         _issuerNameService;
	private readonly IDiscoveryResponseMaker    _responseMaker;
	private readonly IServerUris                _uris;
	private readonly ILogger<DiscoveryEndpoint> _logger;

	public DiscoveryEndpoint(
		FederationOptions          options,
		IIssuerNameService         issuerNameService,
		IDiscoveryResponseMaker    responseMaker,
		IServerUris                uris,
		ILogger<DiscoveryEndpoint> logger)
	{
		_options           = options;
		_issuerNameService = issuerNameService;
		_responseMaker     = responseMaker;
		_uris              = uris;
		_logger            = logger;
	}


	public async Task<IEndpointResult> ProcessAsync(HttpContext context)
	{
		_logger.LogTrace("Processing discovery request");

		if (!HttpMethods.IsGet(context.Request.Method))
		{
			_logger.LogWarning("Discovery endpoint only supports GET requests");
			return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
		}

		_logger.LogDebug("Start discovery request");
		
		if(!_options.Endpoints.EnableDiscoveryEndpoint)
		{
			_logger.LogWarning("Discovery endpoint is disabled. 404 Not Found.");
			return new StatusCodeResult(HttpStatusCode.NotFound);
		}

		var baseUri   = _uris.BaseUri;
		var issuerUri = await _issuerNameService.GetCurrentAsync();

		_logger.LogTrace("Calling into discovery response maker: {Type}", _responseMaker.GetType().FullName);
		var response = await _responseMaker.CreateResultAsync(issuerUri, baseUri);

		return new DiscoveryResult(response, _options.Discovery.ResponseCacheMaxAge);
	}
}