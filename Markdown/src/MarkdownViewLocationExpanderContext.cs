// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Markdown;

public class MarkdownViewLocationExpanderContext
{
   public MarkdownViewLocationExpanderContext(
      ActionContext actionContext,
      string        viewName,
      string?       controllerName,
      string?       areaName,
      string?       pageName,
      bool          isMainPage)
   {
      ActionContext  = actionContext.ThrowIfNull();
      ViewName       = viewName.ThrowIfNull();
      ControllerName = controllerName;
      AreaName       = areaName;
      PageName       = pageName;
      IsMainPage     = isMainPage;
   }

   public ActionContext ActionContext  { get; }
   public string        ViewName       { get; }
   public string?       ControllerName { get; }
   public string?       PageName       { get; }
   public string?       AreaName       { get; }
   public bool          IsMainPage     { get; }

   public IDictionary<string, string?> Values { get; set; } = default!;
}