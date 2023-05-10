// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionEndpointDataSourceFactory
{
	private readonly PageActionEndpointDataSourceIdProvider _dataSourceIdProvider;
	private readonly IActionDescriptorCollectionProvider    _actions;
	private readonly MarkdownActionEndpointFactory                  _endpointFactory;

	public MarkdownActionEndpointDataSourceFactory(
		PageActionEndpointDataSourceIdProvider dataSourceIdProvider,
		IActionDescriptorCollectionProvider    actions,
		MarkdownActionEndpointFactory                  endpointFactory)
	{
		_dataSourceIdProvider = dataSourceIdProvider;
		_actions              = actions;
		_endpointFactory      = endpointFactory;
	}

	public MarkdownActionEndpointDataSource Create(
		OrderedEndpointsSequenceProvider orderProvider)
		=> new MarkdownActionEndpointDataSource(_dataSourceIdProvider, _actions, _endpointFactory, orderProvider);
}