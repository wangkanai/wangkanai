// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown;

public interface IMarkdownViewLocationExpander
{
	void PopulateValues(
		MarkdownViewLocationExpanderContext context);

	IEnumerable<string> ExpandViewLocations(
		MarkdownViewLocationExpanderContext context,
		IEnumerable<string> viewLocations);
}
