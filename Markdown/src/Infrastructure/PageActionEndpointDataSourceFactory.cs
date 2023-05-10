// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.Markdown.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class PageActionEndpointDataSourceFactory
{
	private readonly PageActionEndpointDataSourceIdProvider _dataSourceIdProvider;
	private readonly IActionDescriptorCollectionProvider    _actions;
	private readonly ActionEndpointFactory                  _endpointFactory;

	public PageActionEndpointDataSourceFactory(
		PageActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider    actions,
		ActionEndpointFactory                  endpointFactory)
	{
		_dataSourceIdProvider = dataSourceIdProvider;
		_actions              = actions;
		_endpointFactory      = endpointFactory;
	}

	public PageActionEndpointDataSource Create(OrderedEndpointsSequenceProvider orderProvider)
		=> new PageActionEndpointDataSource(_dataSourceIdProvider, _actions, _endpointFactory, orderProvider);
}