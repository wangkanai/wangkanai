// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Html.Extensions;

namespace Wangkanai.SendGrid.Services;

public sealed class RazorViewRenderService : IRazorViewRenderService
{
    private readonly IServiceProvider  _serviceProvider;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IRazorViewEngine  _viewEngine;

    public RazorViewRenderService(IRazorViewEngine  viewEngine,
                                  ITempDataProvider tempDataProvider,
                                  IServiceProvider  serviceProvider)
    {
        _viewEngine       = viewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider  = serviceProvider;
    }

    public async Task<string> ViewToPlainAsync<TModel>(string viewName, TModel model)
    {
        var html = await ViewToHtmlAsync(viewName, model);
        return HtmlConversionExtensions.ConvertToPlainText(html);
    }

    public async Task<string> ViewToHtmlAsync<TModel>(string viewName, TModel model)
    {
        var actionContext = GetActionContext();
        var view          = FindView(actionContext, viewName);

        await using var output = new StringWriter();

        var provider = new EmptyModelMetadataProvider();
        var state    = new ModelStateDictionary();
        var viewData = new ViewDataDictionary<TModel>(provider, state);
        viewData.Model = model;

        var tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
        var options  = new HtmlHelperOptions();

        var viewContext = new ViewContext(actionContext, view, viewData, tempData, output, options);

        await view.RenderAsync(viewContext);

        return output.ToString();
    }

    private IView FindView(ActionContext action, string viewName)
    {
        var getViewResult = _viewEngine.GetView(null, viewName, true);
        if (getViewResult.Success)
            return getViewResult.View;

        var findViewResult = _viewEngine.FindView(action, viewName, true);
        if (findViewResult.Success)
            return findViewResult.View;

        var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);

        var errorMessage = string.Join(Environment.NewLine, new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }
                                           .Concat(searchedLocations));

        throw new InvalidOperationException(errorMessage);
    }

    private ActionContext GetActionContext()
    {
        var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };

        return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
    }
}