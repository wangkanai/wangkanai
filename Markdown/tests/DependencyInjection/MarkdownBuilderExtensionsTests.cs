// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Xunit;

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown;

public class MarkdownBuilderExtensionsTests
{
	[Fact]
	public void AddMarkdown_Null_Exception()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddMarkdown());
	}

	[Fact]
	public void AddMarkdownBuilder_Null_Exception()
	{
		Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddMarkdownBuilder());
	}
}