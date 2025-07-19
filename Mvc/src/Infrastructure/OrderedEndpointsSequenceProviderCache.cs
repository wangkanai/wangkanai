// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Collections.Concurrent;

using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Mvc.Infrastructure;

internal sealed class OrderedEndpointsSequenceProviderCache
{
	private readonly ConcurrentDictionary<IEndpointRouteBuilder, OrderedEndpointsSequenceProvider> _sequenceProviderCache = new();

	public OrderedEndpointsSequenceProvider GetOrCreateOrderedEndpointsSequenceProvider(IEndpointRouteBuilder endpoints)
	{
		return _sequenceProviderCache.GetOrAdd(endpoints, new OrderedEndpointsSequenceProvider());
	}
}
