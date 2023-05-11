// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionEndpointDataSourceFactory
{
	private readonly MarkdownActionEndpointDataSourceIdProvider _dataSourceIdProvider;
	private readonly IActionDescriptorCollectionProvider    _actions;
	private readonly ActionEndpointFactory                  _endpointFactory;

	public MarkdownActionEndpointDataSourceFactory(
		MarkdownActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider    actions,
		ActionEndpointFactory                  endpointFactory)
	{
		_dataSourceIdProvider = dataSourceIdProvider;
		_actions              = actions;
		_endpointFactory      = endpointFactory;
	}

	public MarkdownActionEndpointDataSource Create(
		OrderedEndpointsSequenceProvider orderProvider)
		=> new MarkdownActionEndpointDataSource(_dataSourceIdProvider, _actions, _endpointFactory, orderProvider);
}