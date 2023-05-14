// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionInvokerCacheEntry
{
	    public MarkdownActionInvokerCacheEntry(
        CompiledMarkdownActionDescriptor                                       actionDescriptor,
        Func<IModelMetadataProvider, ModelStateDictionary, ViewDataDictionary> viewDataFactory,
        Func<MarkdownContext, ViewContext, object>                             markdownFactory,
        Func<MarkdownContext, ViewContext, object, ValueTask>?                 releaseMarkdown,
        Func<MarkdownContext, object>?                                         modelFactory,
        Func<MarkdownContext, object, ValueTask>?                              releaseModel,
        Func<MarkdownContext, object, Task>                                    propertyBinder,
        MarkdownHandlerExecutorDelegate[]                                      handlerExecutors,
        MarkdownHandlerBinderDelegate[]                                        handlerBinders,
        IReadOnlyList<Func<IMarkdownPage>>                                     viewStartFactories,
        FilterItem[]                                                           cacheableFilters)
    {
        ActionDescriptor = actionDescriptor;
        ViewDataFactory = viewDataFactory;
        MarkdownFactory = markdownFactory;
        ReleaseMarkdown = releaseMarkdown;
        ModelFactory = modelFactory;
        ReleaseModel = releaseModel;
        PropertyBinder = propertyBinder;
        HandlerExecutors = handlerExecutors;
        HandlerBinders = handlerBinders;
        ViewStartFactories = viewStartFactories;
        CacheableFilters = cacheableFilters;
    }

    public CompiledMarkdownActionDescriptor ActionDescriptor { get; }

    public Func<MarkdownContext, ViewContext, object> MarkdownFactory { get; }

    /// <summary>
    /// The action invoked to release a page. This may be <c>null</c>.
    /// </summary>
    public Func<MarkdownContext, ViewContext, object, ValueTask>? ReleaseMarkdown { get; }

    public Func<MarkdownContext, object>? ModelFactory { get; }

    /// <summary>
    /// The delegate invoked to release a model. This may be <c>null</c>.
    /// </summary>
    public Func<MarkdownContext, object, ValueTask>? ReleaseModel { get; }

    /// <summary>
    /// The delegate invoked to bind either the handler type (page or model).
    /// This may be <c>null</c>.
    /// </summary>
    public Func<MarkdownContext, object, Task> PropertyBinder { get; }

    public MarkdownHandlerExecutorDelegate[] HandlerExecutors { get; }

    public MarkdownHandlerBinderDelegate[] HandlerBinders { get; }

    public Func<IModelMetadataProvider, ModelStateDictionary, ViewDataDictionary> ViewDataFactory { get; }

    /// <summary>
    /// Gets the applicable ViewStart pages.
    /// </summary>
    public IReadOnlyList<Func<IMarkdownPage>> ViewStartFactories { get; }

    public FilterItem[] CacheableFilters { get; }
}