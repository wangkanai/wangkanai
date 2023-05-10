// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Primitives;

using Wangkanai.Markdown.Routing;

using PageActionEndpointConventionBuilder = Wangkanai.Markdown.Builder.PageActionEndpointConventionBuilder;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class PageActionEndpointDataSource : ActionEndpointDataSourceBase
{
	private readonly PageActionEndpointDataSourceIdProvider _dataSourceIdProvider;
	private readonly ActionEndpointFactory                  _endpointFactory;

	public int                                 DataSourceId   { get; }
	public PageActionEndpointConventionBuilder DefaultBuilder { get; }

	public PageActionEndpointDataSource(
		PageActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider    actions,
		ActionEndpointFactory                  endpointFactory,
		OrderedEndpointsSequenceProvider       orderedEndpoints)
		: base(actions)
	{
		DataSourceId          = dataSourceIdProvider.CreateId();
		_dataSourceIdProvider = dataSourceIdProvider;
		_endpointFactory      = endpointFactory;
		DefaultBuilder        = new PageActionEndpointConventionBuilder(Lock, Conventions, FinallyConventions);
	}


	public override IChangeToken GetChangeToken()
	{
		throw new NotImplementedException();
	}

	protected override List<Endpoint> CreateEndpoints(RoutePattern? groupPrefix, IReadOnlyList<ActionDescriptor> actions, IReadOnlyList<Action<EndpointBuilder>> conventions, IReadOnlyList<Action<EndpointBuilder>> groupConventions, IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
	                                                  IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions)
	{
		throw new NotImplementedException();
	}
}