// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal class DynamicMarkdownEndpointDataSelectorCache
{
	private readonly ConcurrentDictionary<int, EndpointDataSource>              _dataSourceCache       = new();
	private readonly ConcurrentDictionary<int, DynamicMarkdownEndpointSelector> _endpointSelectorCache = new();

	public void AddDataSource(MarkdownActionEndpointDataSource dataSource)
	{
		_dataSourceCache.GetOrAdd(dataSource.DataSourceId, dataSource);
	}
}