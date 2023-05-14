// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownEndpointMatcherPolicy : MatcherPolicy, IEndpointSelectorPolicy
{
	private readonly DynamicMarkdownEndpointSelectorCache _selectorCache;
	private readonly MarkdownLoader                       _loader;
	private readonly EndpointMetadataComparer             _comparer;

	public DynamicMarkdownEndpointMatcherPolicy(
		DynamicMarkdownEndpointSelectorCache selectorCache,
		MarkdownLoader                       loader,
		EndpointMetadataComparer             comparer)
	{
		_selectorCache = selectorCache.ThrowIfNull();
		_loader        = loader.ThrowIfNull();
		_comparer      = comparer.ThrowIfNull();
	}

	public override int Order => int.MinValue + 100;

	public bool AppliesToEndpoints(IReadOnlyList<Endpoint> endpoints)
	{
		endpoints.ThrowIfNull();

		if (!ContainsDynamicEndpoints(endpoints))
			return false;

		for (var i = 0; i < endpoints.Count; i++)
		{
			if (endpoints[i].Metadata.GetMetadata<DynamicMarkdownMetadata>() != null)
				return true;

			if (endpoints[i].Metadata.GetMetadata<DynamicMarkdownRouteValueTransformerMetadata>() != null)
				return true;
		}

		return false;
	}

	public async Task ApplyAsync(HttpContext httpContext, CandidateSet candidates)
	{
		httpContext.ThrowIfNull();
		candidates.ThrowIfNull();

		DynamicMarkdownEndpointSelector? selector = null;

		for (var i = 0; i < candidates.Count; i++)
		{
			if (!candidates.IsValidCandidate(i))
				continue;

			var endpoint       = candidates[i].Endpoint;
			var originalValues = candidates[i].Values;

			RouteValueDictionary? dynamicValues = null;

			var dynamicPageMetadata = endpoint.Metadata.GetMetadata<DynamicMarkdownMetadata>();
			var transformerMetadata = endpoint.Metadata.GetMetadata<DynamicMarkdownRouteValueTransformerMetadata>();

			DynamicRouteValueTransformer? transformer = null;

			if (dynamicPageMetadata != null)
				dynamicValues = dynamicPageMetadata.Values;
			else if (transformerMetadata != null)
			{
				transformer = (DynamicRouteValueTransformer)httpContext.RequestServices.GetRequiredService(transformerMetadata.SelectorType);
				if (transformer.State != null)
					throw new InvalidOperationException(string.Format(Resources.StateShouldBeNullForRouteValueTransformers, transformerMetadata.SelectorType.Name));

				transformer.State = transformerMetadata.State;
				dynamicValues     = await transformer.TransformAsync(httpContext, originalValues!);
			}
			else
				continue;

			if (dynamicValues == null)
			{
				candidates.ReplaceEndpoint(i, null, null);
				continue;
			}

			selector = ResolveSelector(selector, endpoint);
			var endpoints = selector.SelectEndpoints(dynamicValues);
			if (endpoints.Count == 0 && dynamicPageMetadata != null)
			{
				throw new InvalidOperationException(
					"Cannot find the fallback endpoint specified by route values: " +
					"{ "                                                            + string.Join(", ", dynamicValues.Select(kvp => $"{kvp.Key}: {kvp.Value}")) + " }.");
			}

			if (endpoints.Count == 0)
			{
				candidates.ReplaceEndpoint(i, null, null);
				continue;
			}

			var values = new RouteValueDictionary(dynamicValues);

			if (originalValues != null)
				foreach (var kvp in originalValues)
					values.TryAdd(kvp.Key, kvp.Value);

			if (transformer != null)
			{
				endpoints = await transformer.FilterAsync(httpContext, values, endpoints);
				if (endpoints.Count == 0)
				{
					candidates.ReplaceEndpoint(i, null, null);
					continue;
				}
			}
			
			candidates.ReplaceEndpoint(i, endpoint, values);

			var loadedEndpoints = new List<Endpoint>(endpoints);
			for (var j = 0; j < loadedEndpoints.Count; j++)
			{
				var metadata         = loadedEndpoints[j].Metadata;
				var actionDescriptor = metadata.GetMetadata<MarkdownActionDescriptor>();
				if (actionDescriptor is not CompiledMarkdownActionDescriptor)
				{
					var compiled = actionDescriptor!.CompiledMarkdownDescriptor ??
					               await _loader.LoadAsync(actionDescriptor, endpoint.Metadata);
					loadedEndpoints[j] = compiled.Endpoint!;
				}
			}

			candidates.ExpandEndpoint(i, loadedEndpoints, _comparer);
		}
	}

	private DynamicMarkdownEndpointSelector ResolveSelector(DynamicMarkdownEndpointSelector? currentSelector, Endpoint endpoint)
	{
		var selector = _selectorCache.GetEndpointSelector(endpoint);

		Debug.Assert(currentSelector == null || ReferenceEquals(currentSelector, selector));

		return selector!;
	}
}