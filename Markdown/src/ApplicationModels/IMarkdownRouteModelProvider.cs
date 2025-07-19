// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownRouteModelProvider
{
	int Order { get; }

	void OnProvidersExecuting(MarkdownRouteModelProviderContext context);

	void OnProvidersExecuted(MarkdownRouteModelProviderContext context);
}
