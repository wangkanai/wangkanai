// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;

using Wangkanai.Markdown.ApplicationModels;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Wangkanai.Markdown.Infrastructure;

public class MarkdownActionDescriptorProvider : IActionDescriptorProvider
{
	private readonly IMarkdownRouteModelProvider[] _routeModelProviders;
	private readonly MvcOptions _mvcOptions;
	private readonly IMarkdownRouteModelConvention[] _conventions;

	public MarkdownActionDescriptorProvider(
		IEnumerable<IMarkdownRouteModelProvider> markdownRouteModelProviders,
		IOptions<MvcOptions> mvcOptionsAccessor,
		IOptions<MarkdownPagesOptions> MarkdownOptionsAccessor)
	{
		_routeModelProviders = markdownRouteModelProviders.OrderBy(p => p.Order).ToArray();
		_mvcOptions = mvcOptionsAccessor.Value;

		_conventions = MarkdownOptionsAccessor.Value.Conventions
											  .OfType<IMarkdownRouteModelConvention>()
											  .ToArray();
	}

	public int Order { get; set; } = -900;

	public void OnProvidersExecuting(ActionDescriptorProviderContext context)
	{
		var pageRouteModels = BuildModel();

		for (var i = 0; i < pageRouteModels.Count; i++)
			AddActionDescriptors(context.Results, pageRouteModels[i]);
	}

	protected IList<MarkdownRouteModel> BuildModel()
	{
		var context = new MarkdownRouteModelProviderContext();

		for (var i = 0; i < _routeModelProviders.Length; i++)
			_routeModelProviders[i].OnProvidersExecuting(context);

		for (var i = _routeModelProviders.Length - 1; i >= 0; i--)
			_routeModelProviders[i].OnProvidersExecuted(context);

		return context.RouteModels;
	}

	public void OnProvidersExecuted(ActionDescriptorProviderContext context) { }

	private void AddActionDescriptors(IList<ActionDescriptor> actions, MarkdownRouteModel model)
	{
		for (var i = 0; i < _conventions.Length; i++)
			_conventions[i].Apply(model);

		foreach (var selector in model.Selectors)
		{
			var descriptor = new MarkdownActionDescriptor
			{
				ActionConstraints = selector.ActionConstraints.ToList(),
				AreaName = model.AreaName,
				AttributeRouteInfo = new AttributeRouteInfo
				{
					Name = selector.AttributeRouteModel!.Name,
					Order = selector.AttributeRouteModel.Order ?? 0,
					Template = TransformMarkdownRoute(model, selector),
					SuppressLinkGeneration = selector.AttributeRouteModel.SuppressLinkGeneration,
					SuppressPathMatching = selector.AttributeRouteModel.SuppressPathMatching,
				},
				DisplayName = $"Page: {model.ViewEnginePath}",
				EndpointMetadata = selector.EndpointMetadata.ToList(),
				FilterDescriptors = Array.Empty<FilterDescriptor>(),
				Properties = new Dictionary<object, object?>(model.Properties),
				RelativePath = model.RelativePath,
				ViewEnginePath = model.ViewEnginePath,
			};

			foreach (var kvp in model.RouteValues)
				if (!descriptor.RouteValues.ContainsKey(kvp.Key))
					descriptor.RouteValues.Add(kvp.Key, kvp.Value);

			if (!descriptor.RouteValues.ContainsKey("page"))
				descriptor.RouteValues.Add("page", model.ViewEnginePath);

			actions.Add(descriptor);
		}
	}

	private static string? TransformMarkdownRoute(MarkdownRouteModel model, SelectorModel selectorModel)
	{
		if (model.RouteParameterTransformer == null)
			return selectorModel.AttributeRouteModel!.Template;

		var markdownRouteMetadata = selectorModel.EndpointMetadata.OfType<MarkdownRouteMetadata>().SingleOrDefault();
		if (markdownRouteMetadata == null)
			return selectorModel.AttributeRouteModel!.Template;

		var segments = (string?[])markdownRouteMetadata.MarkdownRoute.Split('/');
		for (var i = 0; i < segments.Length; i++)
			segments[i] = model.RouteParameterTransformer.TransformOutbound(segments[i]);

		var transformedPageRoute = string.Join("/", segments);

		return AttributeRouteModel.CombineTemplates(transformedPageRoute, markdownRouteMetadata.RouteTemplate);
	}
}
