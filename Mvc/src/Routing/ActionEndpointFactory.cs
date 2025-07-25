﻿// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;

using Wangkanai.Mvc.Filters;
using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Shared;
using Wangkanai.Routing.Controllers;

namespace Wangkanai.Mvc.Routing;

internal sealed class ActionEndpointFactory
{
	private readonly RoutePatternTransformer _routePatternTransformer;
	private readonly RequestDelegate _requestDelegate;
	private readonly IRequestDelegateFactory[] _requestDelegateFactories;
	private readonly IServiceProvider _serviceProvider;

	public ActionEndpointFactory(
		RoutePatternTransformer routePatternTransformer,
		IEnumerable<IRequestDelegateFactory> requestDelegateFactories,
		IServiceProvider serviceProvider)
	{
		routePatternTransformer.ThrowIfNull(nameof(routePatternTransformer));

		_routePatternTransformer = routePatternTransformer;
		_requestDelegate = CreateRequestDelegate();
		_requestDelegateFactories = requestDelegateFactories.ToArray();
		_serviceProvider = serviceProvider;
	}

	private static RequestDelegate CreateRequestDelegate()
	{
		IActionInvokerFactory? invokerFactory = null;

		return (context) =>
		{
			var endpoint = context.GetEndpoint();
			var dataTokens = endpoint.Metadata.GetMetadata<IDataTokensMetadata>();

			var routeData = new RouteData();
			routeData.PushState(router: null, context.Request.RouteValues, new RouteValueDictionary(dataTokens?.DataTokens));

			var action = endpoint.Metadata.GetMetadata<ActionDescriptor>()!;
			var actionContext = new ActionContext(context, routeData, action);

			if (invokerFactory == null)
				invokerFactory = context.RequestServices.GetRequiredService<IActionInvokerFactory>();

			var invoker = invokerFactory.CreateInvoker(actionContext);
			return invoker!.InvokeAsync();
		};
	}

	private RequestDelegate? CreateRequestDelegate(ActionDescriptor action, RouteValueDictionary? dataTokens = null)
	{
		foreach (var factory in _requestDelegateFactories)
		{
			var requestDelegate = factory.CreateRequestDelegate(action, dataTokens);
			if (requestDelegate != null)
				return requestDelegate;
		}

		return null;
	}

	public void AddEndpoints(
		List<Endpoint> endpoints,
		HashSet<string> routeNames,
		ActionDescriptor action,
		IReadOnlyList<ConventionalRouteEntry> routes,
		IReadOnlyList<Action<EndpointBuilder>> conventions,
		IReadOnlyList<Action<EndpointBuilder>> groupConventions,
		IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
		IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions,
		bool createInertEndpoints,
		RoutePattern? groupPrefix = null)
	{
		endpoints.ThrowIfNull();
		routeNames.ThrowIfNull();
		action.ThrowIfNull();
		routes.ThrowIfNull();
		conventions.ThrowIfNull();
		groupConventions.ThrowIfNull();
		finallyConventions.ThrowIfNull();
		groupFinallyConventions.ThrowIfNull();

		if (createInertEndpoints)
		{
			var builder = new InertEndpointBuilder()
			{
				DisplayName = action.DisplayName,
				RequestDelegate = _requestDelegate,
			};
			AddActionDataToBuilder(
				builder,
				routeNames,
				action,
				routeName: null,
				dataTokens: null,
				suppressLinkGeneration: false,
				suppressPathMatching: false,
				groupConventions: groupConventions,
				conventions: conventions,
				perRouteConventions: Array.Empty<Action<EndpointBuilder>>(),
				groupFinallyConventions: groupFinallyConventions,
				finallyConventions: finallyConventions,
				perRouteFinallyConventions: Array.Empty<Action<EndpointBuilder>>());
			endpoints.Add(builder.Build());
		}

		if (action.AttributeRouteInfo?.Template == null)
		{
			// Check each of the conventional patterns to see if the action would be reachable.
			// If the action and pattern are compatible then create an endpoint with action
			// route values on the pattern.
			foreach (var route in routes)
			{
				// A route is applicable if:
				// 1. It has a parameter (or default value) for 'required' non-null route value
				// 2. It does not have a parameter (or default value) for 'required' null route value
				var updatedRoutePattern = _routePatternTransformer.SubstituteRequiredValues(route.Pattern, action.RouteValues);
				if (updatedRoutePattern == null)
				{
					continue;
				}

				updatedRoutePattern = RoutePatternFactory.Combine(groupPrefix, updatedRoutePattern);

				var requestDelegate = CreateRequestDelegate(action, route.DataTokens) ?? _requestDelegate;

				// We suppress link generation for each conventionally routed endpoint. We generate a single endpoint per-route
				// to handle link generation.
				var builder = new RouteEndpointBuilder(requestDelegate, updatedRoutePattern, route.Order)
				{
					DisplayName = action.DisplayName,
					ApplicationServices = _serviceProvider,
				};
				AddActionDataToBuilder(
					builder,
					routeNames,
					action,
					route.RouteName,
					route.DataTokens,
					suppressLinkGeneration: true,
					suppressPathMatching: false,
					groupConventions: groupConventions,
					conventions: conventions,
					perRouteConventions: route.Conventions,
					groupFinallyConventions: groupFinallyConventions,
					finallyConventions: finallyConventions,
					perRouteFinallyConventions: route.FinallyConventions);
				endpoints.Add(builder.Build());
			}
		}
		else
		{
			var requestDelegate = CreateRequestDelegate(action) ?? _requestDelegate;
			var attributeRoutePattern = RoutePatternFactory.Parse(action.AttributeRouteInfo.Template);

			// Modify the route and required values to ensure required values can be successfully subsituted.
			// Subsitituting required values into an attribute route pattern should always succeed.
			var (resolvedRoutePattern, resolvedRouteValues) = ResolveDefaultsAndRequiredValues(action, attributeRoutePattern);

			var updatedRoutePattern = _routePatternTransformer.SubstituteRequiredValues(resolvedRoutePattern, resolvedRouteValues);
			if (updatedRoutePattern == null)
			{
				// This kind of thing can happen when a route pattern uses a *reserved* route value such as `action`.
				// See: https://github.com/dotnet/aspnetcore/issues/14789
				var formattedRouteKeys = string.Join(", ", resolvedRouteValues.Keys.Select(k => $"'{k}'"));
				throw new InvalidOperationException(
					$"Failed to update the route pattern '{resolvedRoutePattern.RawText}' with required route values. " +
					$"This can occur when the route pattern contains parameters with reserved names such as: {formattedRouteKeys} " +
					$"and also uses route constraints such as '{{action:int}}'. " +
					"To fix this error, choose a different parameter name.");
			}

			updatedRoutePattern = RoutePatternFactory.Combine(groupPrefix, updatedRoutePattern);

			var builder = new RouteEndpointBuilder(requestDelegate, updatedRoutePattern, action.AttributeRouteInfo.Order)
			{
				DisplayName = action.DisplayName,
				ApplicationServices = _serviceProvider,
			};
			AddActionDataToBuilder(
				builder,
				routeNames,
				action,
				action.AttributeRouteInfo.Name,
				dataTokens: null,
				action.AttributeRouteInfo.SuppressLinkGeneration,
				action.AttributeRouteInfo.SuppressPathMatching,
				groupConventions: groupConventions,
				conventions: conventions,
				perRouteConventions: Array.Empty<Action<EndpointBuilder>>(),
				groupFinallyConventions: groupFinallyConventions,
				finallyConventions: finallyConventions,
				perRouteFinallyConventions: Array.Empty<Action<EndpointBuilder>>());
			endpoints.Add(builder.Build());
		}
	}

	private static (RoutePattern resolvedRoutePattern, IDictionary<string, string?> resolvedRequiredValues) ResolveDefaultsAndRequiredValues(ActionDescriptor action, RoutePattern attributeRoutePattern)
	{
		RouteValueDictionary? updatedDefaults = null;
		IDictionary<string, string?>? resolvedRequiredValues = null;

		foreach (var routeValue in action.RouteValues)
		{
			var parameter = attributeRoutePattern.GetParameter(routeValue.Key);

			if (!RouteValueEqualityComparer.Default.Equals(routeValue.Value, string.Empty))
			{
				if (parameter == null)
				{
					// The attribute route has a required value with no matching parameter
					// Add the required values without a parameter as a default
					// e.g.
					//   Template: "Login/{action}"
					//   Required values: { controller = "Login", action = "Index" }
					//   Updated defaults: { controller = "Login" }

					if (updatedDefaults == null)
					{
						updatedDefaults = new RouteValueDictionary(attributeRoutePattern.Defaults);
					}

					updatedDefaults[routeValue.Key] = routeValue.Value;
				}
			}
			else
			{
				if (parameter != null)
				{
					if (resolvedRequiredValues == null)
						resolvedRequiredValues = new Dictionary<string, string?>(action.RouteValues);

					resolvedRequiredValues.Remove(parameter.Name);
				}
			}
		}

		if (updatedDefaults != null)
		{
			attributeRoutePattern = RoutePatternFactory.Parse(action.AttributeRouteInfo!.Template!, updatedDefaults, parameterPolicies: null);
		}

		return (attributeRoutePattern, resolvedRequiredValues ?? action.RouteValues);
	}


	private static void AddActionDataToBuilder(
		EndpointBuilder builder,
		HashSet<string> routeNames,
		ActionDescriptor action,
		string? routeName,
		RouteValueDictionary? dataTokens,
		bool suppressLinkGeneration,
		bool suppressPathMatching,
		IReadOnlyList<Action<EndpointBuilder>> groupConventions,
		IReadOnlyList<Action<EndpointBuilder>> conventions,
		IReadOnlyList<Action<EndpointBuilder>> perRouteConventions,
		IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions,
		IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
		IReadOnlyList<Action<EndpointBuilder>> perRouteFinallyConventions)
	{
		// REVIEW: The RouteEndpointDataSource adds HttpMethodMetadata before running group conventions
		// do we need to do the same here?

		// Group metadata has the lowest precedence.
		for (var i = 0; i < groupConventions.Count; i++)
		{
			groupConventions[i](builder);
		}

		var controllerActionDescriptor = action as ControllerActionDescriptor;

		// Add metadata inferred from the parameter and/or return type before action-specific metadata.
		// MethodInfo *should* never be null given a ControllerActionDescriptor, but this is unenforced.
		if (controllerActionDescriptor?.MethodInfo is not null)
			EndpointMetadataPopulator.PopulateMetadata(controllerActionDescriptor.MethodInfo, builder);

		// Add action-specific metadata early so it has a low precedence
		if (action.EndpointMetadata != null)
			foreach (var d in action.EndpointMetadata)
				builder.Metadata.Add(d);

		builder.Metadata.Add(action);

		if (routeName != null &&
			!suppressLinkGeneration &&
			routeNames.Add(routeName) &&
			builder.Metadata.OfType<IEndpointNameMetadata>().LastOrDefault()?.EndpointName == null)
		{
			builder.Metadata.Add(new EndpointNameMetadata(routeName));
		}

		if (dataTokens != null)
			builder.Metadata.Add(new DataTokensMetadata(dataTokens));

		builder.Metadata.Add(new RouteNameMetadata(routeName));

		// Add filter descriptors to endpoint metadata
		if (action.FilterDescriptors != null && action.FilterDescriptors.Count > 0)
			foreach (var filter in action.FilterDescriptors.OrderBy(f => f, FilterDescriptorOrderComparer.Comparer).Select(f => f.Filter))
				builder.Metadata.Add(filter);

		if (action.ActionConstraints != null && action.ActionConstraints.Count > 0)
		{
			// We explicitly convert a few types of action constraints into MatcherPolicy+Metadata
			// to better integrate with the DFA matcher.
			//
			// Other IActionConstraint data will trigger a back-compat path that can execute
			// action constraints.
			foreach (var actionConstraint in action.ActionConstraints)
			{
				if (actionConstraint is HttpMethodActionConstraint httpMethodActionConstraint &&
					!builder.Metadata.OfType<HttpMethodMetadata>().Any())
				{
					builder.Metadata.Add(new HttpMethodMetadata(httpMethodActionConstraint.HttpMethods));
				}
				else if (actionConstraint is ConsumesAttribute consumesAttribute &&
						 !builder.Metadata.OfType<AcceptsMetadata>().Any())
				{
					builder.Metadata.Add(new AcceptsMetadata(consumesAttribute.ContentTypes.ToArray()));
				}
				else if (!builder.Metadata.Contains(actionConstraint))
				{
					// The constraint might have been added earlier, e.g. it is also a filter descriptor
					builder.Metadata.Add(actionConstraint);
				}
			}
		}

		if (suppressLinkGeneration)
			builder.Metadata.Add(new SuppressLinkGenerationMetadata());

		if (suppressPathMatching)
			builder.Metadata.Add(new SuppressMatchingMetadata());

		for (var i = 0; i < conventions.Count; i++)
			conventions[i](builder);

		for (var i = 0; i < perRouteConventions.Count; i++)
			perRouteConventions[i](builder);

		if (builder.FilterFactories.Count > 0 && controllerActionDescriptor is not null)
		{
			var routeHandlerFilters = builder.FilterFactories;

			EndpointFilterDelegate del = static invocationContext =>
			{
				// By the time this is called, we have the cache entry
				var controllerInvocationContext = (ControllerEndpointFilterInvocationContext)invocationContext;
				return controllerInvocationContext.ActionDescriptor.CacheEntry!.InnerActionMethodExecutor.Execute(controllerInvocationContext);
			};

			var context = new EndpointFilterFactoryContext
			{
				MethodInfo = controllerActionDescriptor.MethodInfo,
				ApplicationServices = builder.ApplicationServices,
			};

			var initialFilteredInvocation = del;

			for (var i = routeHandlerFilters.Count - 1; i >= 0; i--)
			{
				var filterFactory = routeHandlerFilters[i];
				del = filterFactory(context, del);
			}

			controllerActionDescriptor.FilterDelegate = ReferenceEquals(del, initialFilteredInvocation) ? null : del;
		}

		foreach (var perRouteFinallyConvention in perRouteFinallyConventions)
			perRouteFinallyConvention(builder);

		foreach (var finallyConvention in finallyConventions)
			finallyConvention(builder);

		foreach (var groupFinallyConvention in groupFinallyConventions)
			groupFinallyConvention(builder);
	}

	private sealed class InertEndpointBuilder : EndpointBuilder
	{
		public override Endpoint Build()
			=> new(RequestDelegate, new EndpointMetadataCollection(Metadata), DisplayName);
	}
}
