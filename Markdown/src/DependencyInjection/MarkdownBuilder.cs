// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown.Builder;

public sealed class MarkdownBuilder : IMarkdownBuilder
{
   public MarkdownBuilder(IServiceCollection services) => Services = services.ThrowIfNull();

   // PartManager = manager.ThrowIfNull();
   public IServiceCollection Services { get; }
   // public ApplicationPartManager PartManager { get; }
}