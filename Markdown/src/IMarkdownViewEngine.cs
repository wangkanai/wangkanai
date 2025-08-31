// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Wangkanai.Markdown;

public interface IMarkdownViewEngine : IViewEngine
{
   MarkdownPageResult FindPage(ActionContext context, string pageName);

   MarkdownPageResult GetPage(string executingFilePath, string pagePath);

   string? GetAbsolutePath(string? executingFilePath, string? pagePath);
}