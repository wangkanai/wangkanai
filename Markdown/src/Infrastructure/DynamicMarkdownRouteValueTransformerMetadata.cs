// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownRouteValueTransformerMetadata : IDynamicEndpointMetadata
{
   public DynamicMarkdownRouteValueTransformerMetadata(Type selectorType, object? state)
   {
      selectorType.ThrowIfNull();

      if (!typeof(DynamicRouteValueTransformer).IsAssignableFrom(selectorType))
      {
         throw new ArgumentException(
                                     $"The provided type must be a subclass of {typeof(DynamicRouteValueTransformer)}",
                                     nameof(selectorType)
                                    );
      }

      SelectorType = selectorType;
      State        = state;
   }

   public object? State        { get; }
   public Type    SelectorType { get; }
   public bool    IsDynamic    => true;
}