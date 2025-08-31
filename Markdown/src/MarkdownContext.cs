// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown;

public class MarkdownContext : ActionContext
{
   private CompiledMarkdownActionDescriptor? _actionDescriptor;
   private IList<IValueProviderFactory>?     _valueProviderFactories;
   private ViewDataDictionary?               _viewData;
   private IList<Func<IMarkdownPage>>?       _viewStartFactories;

   public MarkdownContext() { }

   public MarkdownContext(ActionContext actionContext)
      : base(actionContext) { }

   internal MarkdownContext(
      HttpContext                      httpContext,
      RouteData                        routeData,
      CompiledMarkdownActionDescriptor actionDescriptor)
      : base(httpContext, routeData, actionDescriptor) =>
      _actionDescriptor = actionDescriptor;

   public new virtual CompiledMarkdownActionDescriptor ActionDescriptor
   {
      get => _actionDescriptor!;
      set => base.ActionDescriptor = _actionDescriptor = value.ThrowIfNull();
   }

   public virtual IList<IValueProviderFactory> ValueProviderFactories
   {
      get
      {
         if (_valueProviderFactories == null)
         {
            _valueProviderFactories = new List<IValueProviderFactory>();
         }

         return _valueProviderFactories;
      }
      set => _valueProviderFactories = value.ThrowIfNull();
   }

   public virtual ViewDataDictionary ViewData
   {
      get => _viewData!;
      set => _viewData = value.ThrowIfNull();
   }

   public virtual IList<Func<IMarkdownPage>> ViewStartFactories
   {
      get => _viewStartFactories!;
      set => _viewStartFactories = value.ThrowIfNull();
   }
}