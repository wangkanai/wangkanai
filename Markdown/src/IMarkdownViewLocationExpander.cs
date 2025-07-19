// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown;

public interface IMarkdownViewLocationExpander
{
	void PopulateValues(
		MarkdownViewLocationExpanderContext context);

	IEnumerable<string> ExpandViewLocations(
		MarkdownViewLocationExpanderContext context,
		IEnumerable<string> viewLocations);
}
