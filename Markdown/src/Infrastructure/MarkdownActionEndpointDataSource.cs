// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Primitives;

using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Markdown.Builder;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionEndpointDataSource : ActionEndpointDataSourceBase
{
	private readonly PageActionEndpointDataSourceIdProvider _dataSourceIdProvider;
	private readonly MarkdownActionEndpointFactory          _endpointFactory;

	public int  DataSourceId         { get; }
	public bool CreateInnerEndpoints { get; set; }

	public MarkdownActionEndpointConventionBuilder DefaultBuilder { get; }

	public MarkdownActionEndpointDataSource(
		PageActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider    actions,
		MarkdownActionEndpointFactory          endpointFactory,
		OrderedEndpointsSequenceProvider       orderedEndpoints)
		: base(actions)
	{
		DataSourceId          = dataSourceIdProvider.CreateId();
		_dataSourceIdProvider = dataSourceIdProvider;
		_endpointFactory      = endpointFactory;
		DefaultBuilder        = new MarkdownActionEndpointConventionBuilder(Lock, Conventions, FinallyConventions);
	}


	public override IChangeToken GetChangeToken()
	{
		throw new NotImplementedException();
	}

	protected override List<Endpoint> CreateEndpoints(
		RoutePattern?                          groupPrefix,
		IReadOnlyList<ActionDescriptor>        actions,
		IReadOnlyList<Action<EndpointBuilder>> conventions,
		IReadOnlyList<Action<EndpointBuilder>> groupConventions,
		IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
		IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions)
	{
		var endpoints  = new List<Endpoint>();
		var routeNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		for (var i = 0; i < actions.Count; i++)
			if (actions[i] is PageActionDescriptor action)
			{
				_endpointFactory.AddEndpoints(
					endpoints,
					routeNames,
					action,
					Array.Empty<ConventionalRouteEntry>(),
					conventions,
					groupConventions,
					finallyConventions,
					groupFinallyConventions,
					CreateInnerEndpoints,
					groupPrefix);
			}

		return endpoints;
	}
}