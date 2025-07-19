// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownRouteModelConvention : IMarkdownConvention
{
	void Apply(MarkdownRouteModel model);
}
