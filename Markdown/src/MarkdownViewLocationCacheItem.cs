// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown;

internal readonly struct MarkdownViewLocationCacheItem
{
   public string Location { get; }

   public Func<IMarkdownPage> PageFactory { get; }

   public MarkdownViewLocationCacheItem(Func<IMarkdownPage> razorPageFactory, string location)
   {
      PageFactory = razorPageFactory;
      Location    = location;
   }
}