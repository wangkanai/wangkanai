// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;

using Wangkanai.Markdown.Builder;
using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionEndpointDataSource : ActionEndpointDataSourceBase
{
	private readonly ActionEndpointFactory _endpointFactory;
	private readonly OrderedEndpointsSequenceProvider _orderSequence;

	public int DataSourceId { get; }
	public bool CreateInertEndpoints { get; set; }

	public MarkdownActionEndpointConventionBuilder DefaultBuilder { get; }

	public MarkdownActionEndpointDataSource(
		MarkdownActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider actions,
		ActionEndpointFactory endpointFactory,
		OrderedEndpointsSequenceProvider orderedEndpoints)
		: base(actions)
	{
		DataSourceId = dataSourceIdProvider.CreateId();
		_endpointFactory = endpointFactory;
		_orderSequence = orderedEndpoints;
		DefaultBuilder = new MarkdownActionEndpointConventionBuilder(Lock, Conventions, FinallyConventions);

		Subscribe();
	}

	protected override List<Endpoint> CreateEndpoints(
		RoutePattern? groupPrefix,
		IReadOnlyList<ActionDescriptor> actions,
		IReadOnlyList<Action<EndpointBuilder>> conventions,
		IReadOnlyList<Action<EndpointBuilder>> groupConventions,
		IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
		IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions)
	{
		var endpoints = new List<Endpoint>();
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
					CreateInertEndpoints,
					groupPrefix);
			}

		return endpoints;
	}

	internal void AddDynamicMarkdownEndpoint(
		IEndpointRouteBuilder endpoints,
		string pattern,
		Type transformerType,
		object? state,
		int? order = null)
	{
		CreateInertEndpoints = true;
		lock (Lock)
		{
			order ??= _orderSequence.GetNext();
			endpoints.Map(pattern, context => throw new InvalidOperationException("This endpoint is not expected to be executed directly."))
					 .Add(b =>
					 {
						 ((RouteEndpointBuilder)b).Order = order.Value;
						 b.Metadata.Add(new DynamicMarkdownRouteValueTransformerMetadata(transformerType, state));
						 b.Metadata.Add(new MarkdownEndpointDataSourceIdMetadata(DataSourceId));
					 });
		}
	}
}
