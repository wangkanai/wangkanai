// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http.Metadata;

namespace Wangkanai.Mvc.Routing;

internal sealed class AcceptsMetadata : IAcceptsMetadata
{
   /// <summary>Creates a new instance of <see cref="AcceptsMetadata"/>.</summary>
   public AcceptsMetadata(string[] contentTypes)
   {
      contentTypes.ThrowIfNull();
      ContentTypes = contentTypes;
   }

   /// <summary>Creates a new instance of <see cref="AcceptsMetadata"/> with a type.</summary>
   public AcceptsMetadata(Type? type, bool isOptional, string[] contentTypes)
   {
      RequestType = type ?? throw new ArgumentNullException(nameof(type));

      contentTypes.ThrowIfNull();

      ContentTypes = contentTypes;
      IsOptional   = isOptional;
   }

   /// <summary>Gets the supported request content types.</summary>
   public IReadOnlyList<string> ContentTypes { get; }

   /// <summary>Gets the type being read from the request.</summary>
   public Type? RequestType { get; }

   /// <summary>Gets a value that determines if the request body is optional.</summary>
   public bool IsOptional { get; }
}