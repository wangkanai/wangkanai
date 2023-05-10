// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class OrderedEndpointsSequenceProviderCache
{
	private readonly ConcurrentDictionary<IEndpointRouteBuilder, OrderedEndpointsSequenceProvider> _sequenceProviderCache = new();

	public OrderedEndpointsSequenceProvider GetOrCreateOrderedEndpointsSequenceProvider(IEndpointRouteBuilder endpoints)
	{
		return _sequenceProviderCache.GetOrAdd(endpoints, new OrderedEndpointsSequenceProvider());
	}
}