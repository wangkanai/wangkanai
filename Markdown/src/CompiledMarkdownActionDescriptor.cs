// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

using Wangkanai.Markdown.Infrastructure;

namespace Wangkanai.Markdown;

public class CompiledMarkdownActionDescriptor : MarkdownActionDescriptor
{
   public CompiledMarkdownActionDescriptor() { }

   public CompiledMarkdownActionDescriptor(MarkdownActionDescriptor other)
      : base(other) { }

   public IList<HandlerMethodDescriptor> HandlerMethods { get; set; } = default!;

   public TypeInfo  HandlerTypeInfo       { get; set; } = default!;
   public TypeInfo? DeclaredModelTypeInfo { get; set; }
   public TypeInfo? ModelTypeInfo         { get; set; }
   public TypeInfo  MarkdownTypeInfo      { get; set; } = default!;
   public Endpoint? Endpoint              { get; set; }

   internal MarkdownActionInvokerCacheEntry? CacheEntry { get; set; }

   internal override CompiledMarkdownActionDescriptor? CompiledMarkdownDescriptor
   {
      get => this;
      set => throw new InvalidOperationException("Setting the compiled descriptor on a compiled descriptor is not allowed.");
   }
}