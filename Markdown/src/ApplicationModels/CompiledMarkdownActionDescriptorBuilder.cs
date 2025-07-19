// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

using Wangkanai.Markdown.Infrastructure;

namespace Wangkanai.Markdown.ApplicationModels;

internal static class CompiledMarkdownActionDescriptorBuilder
{
	public static CompiledMarkdownActionDescriptor Build(
		MarkdownApplicationModel applicationModel,
		FilterCollection globalFilters)
	{
		var boundProperties = CreateBoundProperties(applicationModel);
		var filters = Enumerable.Concat(
									globalFilters.Select(f => new FilterDescriptor(f, FilterScope.Global)),
									applicationModel.Filters.Select(f => new FilterDescriptor(f, FilterScope.Action)))
								.ToArray();
		var handlerMethods = CreateHandlerMethods(applicationModel);

		if (applicationModel.ModelType != null && applicationModel.DeclaredModelType != null &&
			!applicationModel.DeclaredModelType.IsAssignableFrom(applicationModel.ModelType))
		{
			var message = string.Format(Resources.InvalidActionDescriptorModelType,
										applicationModel.ActionDescriptor.DisplayName,
										applicationModel.ModelType.Name,
										applicationModel.DeclaredModelType.Name);

			throw new InvalidOperationException(message);
		}

		var actionDescriptor = applicationModel.ActionDescriptor;
		return new CompiledMarkdownActionDescriptor(actionDescriptor)
		{
			ActionConstraints = actionDescriptor.ActionConstraints,
			AttributeRouteInfo = actionDescriptor.AttributeRouteInfo,
			BoundProperties = boundProperties,
			EndpointMetadata = CreateEndPointMetadata(applicationModel),
			FilterDescriptors = filters,
			HandlerMethods = handlerMethods,
			HandlerTypeInfo = applicationModel.HandlerType,
			DeclaredModelTypeInfo = applicationModel.DeclaredModelType,
			ModelTypeInfo = applicationModel.ModelType,
			RouteValues = actionDescriptor.RouteValues,
			MarkdownTypeInfo = applicationModel.MarkdownType,
			Properties = applicationModel.Properties,
		};
	}

	private static IList<object> CreateEndPointMetadata(MarkdownApplicationModel applicationModel)
	{
		var handlerMetatdata = applicationModel.HandlerTypeAttributes;
		var endpointMetadata = applicationModel.EndpointMetadata;

		return Enumerable.Concat(handlerMetatdata, endpointMetadata).ToList();
	}

	// Internal for unit testing
	internal static HandlerMethodDescriptor[] CreateHandlerMethods(MarkdownApplicationModel applicationModel)
	{
		var handlerModels = applicationModel.HandlerMethods;
		var handlerDescriptors = new HandlerMethodDescriptor[handlerModels.Count];

		for (var i = 0; i < handlerDescriptors.Length; i++)
		{
			var handlerModel = handlerModels[i];

			handlerDescriptors[i] = new HandlerMethodDescriptor
			{
				HttpMethod = handlerModel.HttpMethod,
				Name = handlerModel.HandlerName,
				MethodInfo = handlerModel.MethodInfo,
				Parameters = CreateHandlerParameters(handlerModel),
			};
		}

		return handlerDescriptors;
	}

	// internal for unit testing
	internal static HandlerParameterDescriptor[] CreateHandlerParameters(MarkdownHandlerModel handlerModel)
	{
		var methodParameters = handlerModel.Parameters;
		var parameters = new HandlerParameterDescriptor[methodParameters.Count];

		for (var i = 0; i < parameters.Length; i++)
		{
			var parameterModel = methodParameters[i];

			parameters[i] = new HandlerParameterDescriptor
			{
				BindingInfo = parameterModel.BindingInfo,
				Name = parameterModel.ParameterName,
				ParameterInfo = parameterModel.ParameterInfo,
				ParameterType = parameterModel.ParameterInfo.ParameterType,
			};
		}

		return parameters;
	}

	// internal for unit testing
	internal static MarkdownBoundPropertyDescriptor[] CreateBoundProperties(MarkdownApplicationModel applicationModel)
	{
		var results = new List<MarkdownBoundPropertyDescriptor>();
		var properties = applicationModel.HandlerProperties;
		for (var i = 0; i < properties.Count; i++)
		{
			var propertyModel = properties[i];

			if (propertyModel.BindingInfo == null)
				continue;

			var descriptor = new MarkdownBoundPropertyDescriptor
			{
				Property = propertyModel.PropertyInfo,
				Name = propertyModel.PropertyName,
				BindingInfo = propertyModel.BindingInfo,
				ParameterType = propertyModel.PropertyInfo.PropertyType,
			};

			results.Add(descriptor);
		}

		return results.ToArray();
	}
}
