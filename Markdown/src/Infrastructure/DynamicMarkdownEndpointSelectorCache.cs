// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal class DynamicMarkdownEndpointSelectorCache
{
	private readonly ConcurrentDictionary<int, EndpointDataSource>              _dataSourceCache       = new();
	private readonly ConcurrentDictionary<int, DynamicMarkdownEndpointSelector> _endpointSelectorCache = new();

	public void AddDataSource(MarkdownActionEndpointDataSource dataSource)
		=> _dataSourceCache.GetOrAdd(dataSource.DataSourceId, dataSource);

	internal void AddDataSource(EndpointDataSource dataSource, int key)
		=> _dataSourceCache.GetOrAdd(key, dataSource);

	public DynamicMarkdownEndpointSelector? GetEndpointSelector(Endpoint endpoint)
	{
		if (endpoint?.Metadata == null)
			return null;

		var dataSourceId = endpoint.Metadata.GetMetadata<MarkdownEndpointDataSourceIdMetadata>();
		Debug.Assert(dataSourceId is not null);
		return _endpointSelectorCache.GetOrAdd(dataSourceId.Id, EnsureDataSource);
	}

	private DynamicMarkdownEndpointSelector EnsureDataSource(int key)
	{
		if (!_dataSourceCache.TryGetValue(key, out var dataSource))
			throw new InvalidOperationException($"Data source with key '{key}' not registered.");

		return new DynamicMarkdownEndpointSelector(dataSource);
	}
}