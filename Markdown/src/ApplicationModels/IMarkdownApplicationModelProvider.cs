// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownApplicationModelProvider
{
	int Order { get; }

	void OnProvidersExecuting(MarkdownApplicationModelProviderContext context);

	void OnProvidersExecuted(MarkdownApplicationModelProviderContext context);
}
