// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Mvc.Infrastructure;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownEndpointSelector : IDisposable
{
	private readonly EndpointDataSource                                       _dataSource;
	private readonly DataSourceDependentCache<ActionSelectionTable<Endpoint>> _cache;
	
	public DynamicMarkdownEndpointSelector(EndpointDataSource dataSource)
	{
		DataSource = dataSource;
	}

	public EndpointDataSource DataSource { get; }
	public void Dispose()
	{
		throw new NotImplementedException();
	}
}