// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownMetadata : IDynamicEndpointMetadata
{
   public DynamicMarkdownMetadata(RouteValueDictionary values)
   {
      values.ThrowIfNull();
      Values = values;
   }

   public RouteValueDictionary Values    { get; }
   public bool                 IsDynamic => true;
}