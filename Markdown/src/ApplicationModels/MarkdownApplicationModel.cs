// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Microsoft.AspNetCore.Mvc.Filters;

using Wangkanai.Internal;

namespace Wangkanai.Markdown.ApplicationModels;

public class MarkdownApplicationModel
{
	public MarkdownApplicationModel(
		MarkdownActionDescriptor actionDescriptor,
		TypeInfo                 handlerType,
		IReadOnlyList<object>    handlerAttributes)
		: this(actionDescriptor, handlerType, handlerType, handlerAttributes) { }

	public MarkdownApplicationModel(
		MarkdownActionDescriptor actionDescriptor,
		TypeInfo                 declaredModelType,
		TypeInfo                 handlerType,
		IReadOnlyList<object>    handlerAttributes)
	{
		ActionDescriptor  = actionDescriptor.ThrowIfNull();
		DeclaredModelType = declaredModelType;
		HandlerType       = handlerType;

		Filters = new List<IFilterMetadata>();
		Properties = new CopyOnWriteDictionary<object, object?>(
			actionDescriptor.Properties,
			EqualityComparer<object>.Default);
		HandlerMethods        = new List<MarkdownHandlerModel>();
		HandlerProperties     = new List<MarkdownPropertyModel>();
		HandlerTypeAttributes = handlerAttributes;
		EndpointMetadata      = new List<object>(ActionDescriptor.EndpointMetadata ?? Array.Empty<object>());
	}

	public MarkdownApplicationModel(MarkdownApplicationModel other)
	{
		other.ThrowIfNull();

		ActionDescriptor = other.ActionDescriptor;
		HandlerType      = other.HandlerType;
		MarkdownType     = other.MarkdownType;
		ModelType        = other.ModelType;

		Filters    = new List<IFilterMetadata>(other.Filters);
		Properties = new Dictionary<object, object?>(other.Properties);

		HandlerMethods        = new List<MarkdownHandlerModel>(other.HandlerMethods.Select(m => new MarkdownHandlerModel(m)));
		HandlerProperties     = new List<MarkdownPropertyModel>(other.HandlerProperties.Select(p => new MarkdownPropertyModel(p)));
		HandlerTypeAttributes = other.HandlerTypeAttributes;
		EndpointMetadata      = new List<object>(other.EndpointMetadata);
	}

	public MarkdownActionDescriptor ActionDescriptor { get; }

	public string  RelativePath   => ActionDescriptor.RelativePath;
	public string  ViewEnginePath => ActionDescriptor.ViewEnginePath;
	public string? AreaName       => ActionDescriptor.AreaName;
	public string? RouteTemplate  => ActionDescriptor.AttributeRouteInfo?.Template;

	public IList<IFilterMetadata> Filters { get; }

	public IDictionary<object, object?> Properties { get; }

	public TypeInfo  MarkdownType      { get; set; } = default!;
	public TypeInfo? DeclaredModelType { get; }
	public TypeInfo? ModelType         { get; set; }
	public TypeInfo  HandlerType       { get; }

	public IReadOnlyList<object> HandlerTypeAttributes { get; }

	public IList<MarkdownHandlerModel>  HandlerMethods    { get; }
	public IList<MarkdownPropertyModel> HandlerProperties { get; }
	public IList<object>                EndpointMetadata  { get; }
}