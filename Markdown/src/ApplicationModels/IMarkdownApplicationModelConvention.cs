// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Markdown.ApplicationModels;

public interface IMarkdownApplicationModelConvention : IMarkdownConvention
{
	void Apply(MarkdownApplicationModel model);
}