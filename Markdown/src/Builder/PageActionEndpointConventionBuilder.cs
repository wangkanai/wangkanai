// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Builder;

public sealed class PageActionEndpointConventionBuilder : IEndpointConventionBuilder
{
	private readonly object                        _lock;
	private readonly List<Action<EndpointBuilder>> _conventions;
	private readonly List<Action<EndpointBuilder>> _finallyConventions;

	internal PageActionEndpointConventionBuilder(
		object                        @lock,
		List<Action<EndpointBuilder>> conventions,
		List<Action<EndpointBuilder>> finallyConventions)
	{
		_lock                    = @lock;
		_conventions             = conventions;
		_finallyConventions = finallyConventions;
	}

	public void Add(Action<EndpointBuilder> convention)
	{
		convention.ThrowIfNull();

		lock (_lock)
			_conventions.Add(convention);
	}

	public void Finally(Action<EndpointBuilder> finalConvention)
	{
		finalConvention.ThrowIfNull();

		lock(_lock)
			_finallyConventions.Add(finalConvention);
	}
}