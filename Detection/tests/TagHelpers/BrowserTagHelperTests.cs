// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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

	private static readonly TagHelperOutput Output = new(
		"div",
		new TagHelperAttributeList(),
		(result, encoder) => {
			var tagHelperContent = new DefaultTagHelperContent();
			tagHelperContent.SetHtmlContent(string.Empty);
			return Task.FromResult<TagHelperContent>(tagHelperContent);
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
		Assert.Throws<ArgumentNullException>(() => tagHelper.Process(null!, Output));
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
		tagHelper.Process(Context, Output);
		Assert.Null(Output.TagName);
	}
	
	[Fact]
	public void ProcessIncludeIsEmpty()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = string.Empty;
		tagHelper.Process(Context, Output);
		Assert.Null(Output.TagName);
	}
	
	[Fact]
	public void ProcessIncludeIsWhiteSpace()
	{
		var resolver  = MockService.BrowserService(Agent);
		var tagHelper = new BrowserTagHelper(resolver);
		tagHelper.Include = " ";
		tagHelper.Process(Context, Output);
		Assert.Null(Output.TagName);
	}
}