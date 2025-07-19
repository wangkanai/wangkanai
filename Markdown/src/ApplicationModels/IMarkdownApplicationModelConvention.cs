// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownApplicationModelConvention : IMarkdownConvention
{
	void Apply(MarkdownApplicationModel model);
}
