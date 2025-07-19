// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.TagHelpers;

using Wangkanai.Detection.Mocks;

namespace Wangkanai.Detection.TagHelpers;

public class BrowserTagHelperTests
{
	private const string Agent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Chrome/1.0.154.53 Safari/525.19";
	private const string Tag = "<p>Chrome</p>";

	[Fact]
	public void ResolverIsNull()
	{
		Assert.Throws<ArgumentNullException>(() => new BrowserTagHelper(null!));
	}

	[Fact]
	public void ProcessContextIsNull()
	{
		var resolver = MockService.BrowserService("test");
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(string.Empty);
		Assert.Throws<ArgumentNullException>(() => tagHelper.Process(null!, output));
	}

	[Fact]
	public void ProcessOutputIsNull()
	{
		var resolver = MockService.BrowserService("test");
		var tagHelper = new BrowserTagHelper(resolver);
		var context = MockTagHelper.Context;
		Assert.Throws<ArgumentNullException>(() => tagHelper.Process(context, null!));
	}

	[Fact]
	public void ProcessIncludeIsNull()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(string.Empty);
		var context = MockTagHelper.Context;
		tagHelper.Include = null;
		tagHelper.Process(context, output);
		Assert.Null(output.TagName);
	}

	[Fact]
	public void ProcessIncludeIsEmpty()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(string.Empty);
		var context = MockTagHelper.Context;
		tagHelper.Include = string.Empty;
		tagHelper.Process(context, output);
		Assert.Null(output.TagName);
	}

	[Fact]
	public void ProcessIncludeIsWhiteSpace()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(string.Empty);
		var context = MockTagHelper.Context;
		tagHelper.Include = " ";
		tagHelper.Process(context, output);
		Assert.Null(output.TagName);
	}

	[Fact]
	public void ProcessIncludeIsNotMatch()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(string.Empty);
		var context = MockTagHelper.Context;
		tagHelper.Include = "test";
		tagHelper.Process(context, output);
		Assert.Null(output.TagName);
	}

	// Re-enabled after verification: Test ensures correct behavior when Include matches "chrome".
	[Fact]
	public void ProcessIncludeIsMatch()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Include = "chrome";
		tagHelper.Process(context, output);
		Assert.Equal(Tag, output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessExcludeIsNull()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Exclude = null;
		tagHelper.Process(context, output);
		Assert.Equal(Tag, output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessExcludeIsEmpty()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Exclude = string.Empty;
		tagHelper.Process(context, output);
		Assert.Equal(Tag, output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessExcludeIsWhiteSpace()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Exclude = " ";
		tagHelper.Process(context, output);
		Assert.Equal(Tag, output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessExcludeIsNotMatch()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Exclude = "test";
		tagHelper.Process(context, output);
		Assert.Equal(Tag, output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessExcludeIsMatch()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Exclude = "chrome";
		tagHelper.Process(context, output);
		Assert.Empty(output.Content.GetContent());
	}

	// [Fact]
	[Fact]
	public void ProcessIncludeAndExcludeIsMatch()
	{
		var resolver = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		var output = MockTagHelper.Output(Tag);
		var context = MockTagHelper.Context;
		tagHelper.Include = "chrome";
		tagHelper.Exclude = "chrome";
		tagHelper.Process(context, output);
		Assert.Empty(output.Content.GetContent());
	}
}
