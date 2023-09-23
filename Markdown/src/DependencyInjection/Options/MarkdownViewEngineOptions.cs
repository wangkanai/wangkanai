// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown.DependencyInjection.Options;

public class MarkdownViewEngineOptions
{
	public List<IMarkdownViewLocationExpander> ViewLocationExpanders { get; } = new();

	public List<string> ViewLocationFormats         { get; } = new();
	public List<string> AreaViewLocationFormats     { get; } = new();
	public List<string> PageViewLocationFormats     { get; } = new();
	public List<string> AreaPageViewLocationFormats { get; } = new();
}