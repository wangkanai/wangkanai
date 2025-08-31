// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;

using Wangkanai.Markdown.ApplicationModels;
using Wangkanai.Markdown.DependencyInjection.Options;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class CompiledMarkdownActionDescriptorFactory
{
   private readonly IMarkdownApplicationModelProvider[] _applicationModelProviders;
   private readonly MarkdownConventionCollection        _conventions;
   private readonly FilterCollection                    _globalFilters;

   public CompiledMarkdownActionDescriptorFactory(
      IEnumerable<IMarkdownApplicationModelProvider> applicationModelProviders,
      MvcOptions                                     mvcOptions,
      MarkdownPagesOptions                           pageOptions)
   {
      _applicationModelProviders = applicationModelProviders.OrderBy(a => a.Order).ToArray();
      _conventions               = pageOptions.Conventions;
      _globalFilters             = mvcOptions.Filters;
   }

   public CompiledMarkdownActionDescriptor CreateCompiledDescriptor(
      MarkdownActionDescriptor actionDescriptor,
      CompiledViewDescriptor   viewDescriptor)
   {
      var context = new MarkdownApplicationModelProviderContext(actionDescriptor, viewDescriptor.Type!.GetTypeInfo());
      for (var i = 0; i < _applicationModelProviders.Length; i++)
         _applicationModelProviders[i].OnProvidersExecuting(context);

      for (var i = _applicationModelProviders.Length - 1; i >= 0; i--)
         _applicationModelProviders[i].OnProvidersExecuted(context);

      ApplyConventions(_conventions, context.MarkdownApplicationModel);

      var compiled = CompiledMarkdownActionDescriptorBuilder.Build(context.MarkdownApplicationModel, _globalFilters);
      actionDescriptor.CompiledMarkdownDescriptor = compiled;

      return compiled;
   }

   internal static void ApplyConventions(
      MarkdownConventionCollection conventions,
      MarkdownApplicationModel     pageApplicationModel)
   {
      var applicationModelConventions = GetConventions<IMarkdownApplicationModelConvention>(pageApplicationModel.HandlerTypeAttributes);
      foreach (var convention in applicationModelConventions)
         convention.Apply(pageApplicationModel);

      var handlers = pageApplicationModel.HandlerMethods.ToArray();
      foreach (var handlerModel in handlers)
      {
         var handlerModelConventions = GetConventions<IMarkdownHandlerModelConvention>(handlerModel.Attributes);
         foreach (var convention in handlerModelConventions)
            convention.Apply(handlerModel);

         var parameterModels = handlerModel.Parameters.ToArray();
         foreach (var parameterModel in parameterModels)
         {
            var parameterModelConventions = GetConventions<IParameterModelBaseConvention>(parameterModel.Attributes);
            foreach (var convention in parameterModelConventions)
               convention.Apply(parameterModel);
         }
      }

      var properties = pageApplicationModel.HandlerProperties.ToArray();
      foreach (var propertyModel in properties)
      {
         var propertyModelConventions = GetConventions<IParameterModelBaseConvention>(propertyModel.Attributes);
         foreach (var convention in propertyModelConventions)
            convention.Apply(propertyModel);
      }

      IEnumerable<TConvention> GetConventions<TConvention>(IReadOnlyList<object> attributes)
         => conventions.OfType<TConvention>().Concat(attributes.OfType<TConvention>());
   }
}