// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown;

internal sealed class MarkdownViewLocationCacheResult
{
	public bool Success { get; }

	public MarkdownViewLocationCacheItem ViewEntry { get; }
	public IReadOnlyList<MarkdownViewLocationCacheItem>? ViewStartEntries { get; }
	public IEnumerable<string>? SearchedLocations { get; }

	public MarkdownViewLocationCacheResult(
		MarkdownViewLocationCacheItem view,
		IReadOnlyList<MarkdownViewLocationCacheItem> viewStarts)
	{
		viewStarts.ThrowIfNull();

		ViewEntry = view;
		ViewStartEntries = viewStarts;
		Success = true;
	}

	public MarkdownViewLocationCacheResult(IEnumerable<string> searchedLocations)
	{
		searchedLocations.ThrowIfNull();

		SearchedLocations = searchedLocations;
	}
}
