// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.Markdown.ApplicationModels;

public class MarkdownApplicationModelProviderContext
{
	public MarkdownApplicationModelProviderContext(MarkdownActionDescriptor descriptor, TypeInfo pageTypeInfo)
	{
		ActionDescriptor = descriptor;
		PageType         = pageTypeInfo;
	}

	public MarkdownActionDescriptor ActionDescriptor { get; }
	
	public TypeInfo PageType { get; }
	
	public MarkdownApplicationModel MarkdownApplicationModel { get; set; } = default!;
}