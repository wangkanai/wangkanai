// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.Extensions.Options;

using Wangkanai.Markdown.ApplicationModels;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Wangkanai.Markdown.Infrastructure;

public sealed class CompiledMarkdownActionDescriptorProvider : IActionDescriptorProvider
{
	private readonly MarkdownActionDescriptorProvider _pageActionDescriptorProvider;
	private readonly ApplicationPartManager _applicationPartManager;
	private readonly CompiledMarkdownActionDescriptorFactory _compiledPageActionDescriptorFactory;

	public CompiledMarkdownActionDescriptorProvider(
		IEnumerable<IMarkdownRouteModelProvider> markdownRouteModelProviders,
		IEnumerable<IMarkdownApplicationModelProvider> applicationModelProviders,
		ApplicationPartManager applicationPartManager,
		IOptions<MvcOptions> mvcOptions,
		IOptions<MarkdownPagesOptions> pageOptions)
	{
		_pageActionDescriptorProvider = new MarkdownActionDescriptorProvider(markdownRouteModelProviders, mvcOptions, pageOptions);
		_applicationPartManager = applicationPartManager;
		_compiledPageActionDescriptorFactory = new CompiledMarkdownActionDescriptorFactory(applicationModelProviders, mvcOptions.Value, pageOptions.Value);
	}

	public int Order => _pageActionDescriptorProvider.Order;

	public void OnProvidersExecuting(ActionDescriptorProviderContext context)
	{
		var newContext = new ActionDescriptorProviderContext();
		_pageActionDescriptorProvider.OnProvidersExecuting(newContext);
		_pageActionDescriptorProvider.OnProvidersExecuted(newContext);

		var feature = new ViewsFeature();
		_applicationPartManager.PopulateFeature(feature);

		var lookup = new Dictionary<string, CompiledViewDescriptor>(feature.ViewDescriptors.Count, StringComparer.Ordinal);

		foreach (var viewDescriptor in feature.ViewDescriptors)
			lookup.TryAdd(ViewPath.NormalizePath(viewDescriptor.RelativePath), viewDescriptor);

		foreach (var item in newContext.Results)
		{
			var pageActionDescriptor = (MarkdownActionDescriptor)item;
			if (!lookup.TryGetValue(pageActionDescriptor.RelativePath, out var compiledViewDescriptor))
				throw new InvalidOperationException($"A descriptor for '{pageActionDescriptor.RelativePath}' was not found.");

			var compiledPageActionDescriptor = _compiledPageActionDescriptorFactory.CreateCompiledDescriptor(
				pageActionDescriptor,
				compiledViewDescriptor);
			context.Results.Add(compiledPageActionDescriptor);
		}
	}

	public void OnProvidersExecuted(ActionDescriptorProviderContext context) { }
}
