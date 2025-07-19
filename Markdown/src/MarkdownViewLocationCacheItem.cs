// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown;

internal readonly struct MarkdownViewLocationCacheItem
{
	public string Location { get; }

	public Func<IMarkdownPage> PageFactory { get; }

	public MarkdownViewLocationCacheItem(Func<IMarkdownPage> razorPageFactory, string location)
	{
		PageFactory = razorPageFactory;
		Location = location;
	}
}
