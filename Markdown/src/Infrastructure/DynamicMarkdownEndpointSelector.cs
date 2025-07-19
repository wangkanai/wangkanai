// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownEndpointSelector : IDisposable
{
	public EndpointDataSource DataSource => _dataSource;

	private readonly DataSourceDependentCache<ActionSelectionTable<Endpoint>> _cache;
	private readonly EndpointDataSource _dataSource;

	public DynamicMarkdownEndpointSelector(EndpointDataSource dataSource)
	{
		dataSource.ThrowIfNull();

		_dataSource = dataSource;
		_cache = new DataSourceDependentCache<ActionSelectionTable<Endpoint>>(dataSource, Initialize);
	}

	private static ActionSelectionTable<Endpoint> Initialize(IReadOnlyList<Endpoint> endpoints)
		=> ActionSelectionTable<Endpoint>.Create(endpoints);

	private ActionSelectionTable<Endpoint> Table
		=> _cache.EnsureInitialized();

	public IReadOnlyList<Endpoint> SelectEndpoints(RouteValueDictionary values)
	{
		values.ThrowIfNull();

		var table = Table;
		var matches = table.Select(values);
		return matches;
	}

	public void Dispose()
	{
		_cache.Dispose();
	}
}
