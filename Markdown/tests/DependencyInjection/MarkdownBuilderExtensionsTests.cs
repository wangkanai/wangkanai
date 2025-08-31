// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Markdown;

public class MarkdownBuilderExtensionsTests
{
   [Fact]
   public void AddMarkdown_Null_Exception() => Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddMarkdownPages());

   [Fact]
   public void AddMarkdownBuilder_Null_Exception() => Assert.Throws<ArgumentNullException>(() => ((IServiceCollection)null).AddMarkdownBuilder());
}