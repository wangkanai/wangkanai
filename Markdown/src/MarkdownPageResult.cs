// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown;

public readonly struct MarkdownPageResult
{
	public string         Name { get; }
	public IMarkdownPage? Page { get; }

	public IEnumerable<string>? SearchedLocations { get; }

	public MarkdownPageResult(string name, IMarkdownPage page)
	{
		Name              = name.ThrowIfNull();
		Page              = page.ThrowIfNull();
		SearchedLocations = null;
	}

	public MarkdownPageResult(string name, IEnumerable<string> searchedLocations)
	{
		Name              = name.ThrowIfNull();
		Page              = null;
		SearchedLocations = searchedLocations.ThrowIfNull();
	}
}