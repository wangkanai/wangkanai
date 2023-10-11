// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Wangkanai.Detection.Mocks;

namespace Wangkanai.Detection.Tests.TagHelpers;

public class BrowserTagHelperTests
{
	private static readonly TagHelperContext Context = new(
		new TagHelperAttributeList(),
		new Dictionary<object, object>(),
		Guid.NewGuid().ToString("N"));

	private static readonly TagHelperOutput OutputEmpty = new(
		"div",
		new TagHelperAttributeList(),
		(result, encoder) => {
			var tagHelperContent = new DefaultTagHelperContent();
			tagHelperContent.SetHtmlContent(string.Empty);
			return Task.FromResult<TagHelperContent>(tagHelperContent);
		});

	private static readonly TagHelperOutput OutputChrome = new(
		"div",
		new TagHelperAttributeList(),
		(result, encoder) => {
			var defaultContent= new DefaultTagHelperContent();
			var content          = "<p>Chrome</p>";
			var helperContent    = defaultContent.SetHtmlContent(content);
			return Task.FromResult(helperContent);
		});

	private const string Agent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/525.19 (KHTML, like Gecko) Chrome/1.0.154.53 Safari/525.19";

	[Fact]
	public void ResolverIsNull()
	{
		Assert.Throws<ArgumentNullException>(() => new BrowserTagHelper(null!));
	}

	[Fact]
	public void ProcessContextIsNull()
	{
		var resolver  = MockService.BrowserService("test");
		var tagHelper = new BrowserTagHelper(resolver);
		Assert.Throws<ArgumentNullException>(() => tagHelper.Process(null!, OutputEmpty));
	}

	[Fact]
	public void ProcessOutputIsNull()
	{
		var resolver  = MockService.BrowserService("test");
		var tagHelper = new BrowserTagHelper(resolver);
		Assert.Throws<ArgumentNullException>(() => tagHelper.Process(Context, null!));
	}

	[Fact]
	public void ProcessIncludeIsNull()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = null;
		tagHelper.Process(Context, OutputEmpty);
		Assert.Null(OutputEmpty.TagName);
	}

	[Fact]
	public void ProcessIncludeIsEmpty()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = string.Empty;
		tagHelper.Process(Context, OutputEmpty);
		Assert.Null(OutputEmpty.TagName);
	}

	[Fact]
	public void ProcessIncludeIsWhiteSpace()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = " ";
		tagHelper.Process(Context, OutputEmpty);
		Assert.Null(OutputEmpty.TagName);
	}

	[Fact]
	public void ProcessIncludeIsNotMatch()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = "test";
		tagHelper.Process(Context, OutputEmpty);
		Assert.Null(OutputEmpty.TagName);
	}

	[Fact]
	public void ProcessIncludeIsMatch()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = "chrome";
		tagHelper.Process(Context, OutputChrome);
		Assert.Equal("<p>Chrome</p>", OutputChrome.Content.GetContent());
	}

	[Fact]
	public void ProcessExcludeIsNull()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Exclude = null;
		tagHelper.Process(Context, OutputChrome);
		Assert.Equal("<p>Chrome</p>", OutputChrome.Content.GetContent());
	}

	[Fact]
	public void ProcessExcludeIsEmpty()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Exclude = string.Empty;
		tagHelper.Process(Context, OutputChrome);
		Assert.Equal("<p>Chrome</p>", OutputChrome.Content.GetContent());
	}

	[Fact]
	public void ProcessExcludeIsWhiteSpace()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Exclude = " ";
		tagHelper.Process(Context, OutputChrome);
		Assert.Equal("<p>Chrome</p>", OutputChrome.Content.GetContent());
	}

	[Fact]
	public void ProcessExcludeIsNotMatch()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Exclude = "test";
		tagHelper.Process(Context, OutputChrome);
		Assert.Equal("<p>Chrome</p>", OutputChrome.Content.GetContent());
	}

	[Fact]
	public void ProcessExcludeIsMatch()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Exclude = "chrome";
		tagHelper.Process(Context, OutputChrome);
		Assert.Null(OutputChrome.TagName);
	}

	[Fact]
	public void ProcessIncludeAndExcludeIsMatch()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = "chrome";
		tagHelper.Exclude = "chrome";
		tagHelper.Process(Context, OutputChrome);
		Assert.Null(OutputChrome.TagName);
	}
}