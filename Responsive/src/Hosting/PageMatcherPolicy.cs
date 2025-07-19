// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;

using Wangkanai.Responsive.Extensions;

namespace Wangkanai.Responsive.Hosting;

internal sealed class ResponsivePageMatcherPolicy : MatcherPolicy, IEndpointComparerPolicy, IEndpointSelectorPolicy
{
	public ResponsivePageMatcherPolicy()
	{
		Comparer = EndpointMetadataComparer<IResponsiveMetadata>.Default;
	}

	public override int Order => 10000;

	public IComparer<Endpoint> Comparer { get; }

	public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
	{
		endpoints.ThrowIfNull();

		return endpoints.Any(endpoint => endpoint.Metadata.GetMetadata<IResponsiveMetadata>() != null);
	}

	public Task ApplyAsync(HttpContext context, CandidateSet candidates)
	{
		context.ThrowIfNull();
		candidates.ThrowIfNull();

		var device = context.GetDevice();

		for (var i = 0; i < candidates.Count; i++)
		{
			var endpoint = candidates[i].Endpoint;
			var metadata = endpoint.Metadata.GetMetadata<IResponsiveMetadata>();
			if (metadata is null)
				continue;
			// This endpoint is not a match for the selected device.
			if (metadata?.Device != null && device != metadata.Device)
				candidates.SetValidity(i, false);
		}

		return Task.CompletedTask;
	}
}
