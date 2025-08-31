// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownRouteModelProvider
{
   int Order { get; }

   void OnProvidersExecuting(MarkdownRouteModelProviderContext context);

   void OnProvidersExecuted(MarkdownRouteModelProviderContext context);
}