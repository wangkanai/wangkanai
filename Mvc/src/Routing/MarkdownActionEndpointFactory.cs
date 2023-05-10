// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Mvc.Routing;

internal sealed class MarkdownActionEndpointFactory
{
	private readonly RoutePatternTransformer   _routePatternTransformer;
	private readonly RequestDelegate           _requestDelegate;
	private readonly IRequestDelegateFactory[] _requestDelegateFactories;
	private readonly IServiceProvider          _serviceProvider;

	public MarkdownActionEndpointFactory(
		RoutePatternTransformer              routePatternTransformer,
		IEnumerable<IRequestDelegateFactory> requestDelegateFactories,
		IServiceProvider                     serviceProvider)
	{
		routePatternTransformer.ThrowIfNull(nameof(routePatternTransformer));

		_routePatternTransformer  = routePatternTransformer;
		_requestDelegate          = CreateRequestDelegate();
		_requestDelegateFactories = requestDelegateFactories.ToArray();
		_serviceProvider          = serviceProvider;
	}

	private static RequestDelegate CreateRequestDelegate()
	{
		IActionInvokerFactory? invokerFactory = null;

		return (context) => {
			var endpoint   = context.GetEndpoint();
			var dataTokens = endpoint.Metadata.GetMetadata<IDataTokensMetadata>();

			var routeData = new RouteData();
			routeData.PushState(router: null, context.Request.RouteValues, new RouteValueDictionary(dataTokens?.DataTokens));

			var action        = endpoint.Metadata.GetMetadata<ActionDescriptor>()!;
			var actionContext = new ActionContext(context, routeData, action);

			if (invokerFactory == null)
				invokerFactory = context.RequestServices.GetRequiredService<IActionInvokerFactory>();

			var invoker = invokerFactory.CreateInvoker(actionContext);
			return invoker!.InvokeAsync();
		};
	}

	private RequestDelegate? CreateRequestDelegate(ActionDescriptor action, RouteValueDictionary? dataTokens = null)
	{
		foreach (var factory in _requestDelegateFactories)
		{
			var requestDelegate = factory.CreateRequestDelegate(action, dataTokens);
			if (requestDelegate != null)
				return requestDelegate;
		}

		return null;
	}

	public void AddEndpoints(
		List<Endpoint> endpoints, 
		HashSet<string> routeNames,
		ActionDescriptor action,
		IReadOnlyList<ConventionalRouteEntry> routes)
	{
	}
}