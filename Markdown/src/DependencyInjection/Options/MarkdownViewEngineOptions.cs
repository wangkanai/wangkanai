// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Razor;

namespace Wangkanai.Markdown.DependencyInjection.Options;

public class MarkdownViewEngineOptions
{
	public IList<IViewLocationExpander> ViewLocationExpanders       { get; } = new List<IViewLocationExpander>();
	public IList<string>                ViewLocationFormats         { get; } = new List<string>();
	public IList<string>                AreaViewLocationFormats     { get; } = new List<string>();
	public IList<string>                PageViewLocationFormats     { get; } = new List<string>();
	public IList<string>                AreaPageViewLocationFormats { get; } = new List<string>();
}