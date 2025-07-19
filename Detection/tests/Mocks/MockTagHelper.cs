// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Wangkanai.Detection.Mocks;

public static class MockTagHelper
{
	internal static TagHelperContext Context
		=> new(
			new TagHelperAttributeList(),
			new Dictionary<object, object>(),
			Guid.NewGuid().ToString("N"));

	internal static TagHelperOutput Output(string content)
		=> new(
			"div",
			new TagHelperAttributeList(),
			(result, encoder) => GetChildContentAsync(content));

	internal static Task<TagHelperContent> GetChildContentAsync(string inner)
		=> GetChildContentAsync(ContentBuilder(inner));

	internal static Task<TagHelperContent> GetChildContentAsync(IHtmlContent inner)
	{
		var content = new DefaultTagHelperContent();
		var helperContent = content.SetHtmlContent(inner);
		return Task.FromResult<TagHelperContent>(helperContent);
	}

	internal static IHtmlContent ContentBuilder(string inner)
	{
		var content = new DefaultTagHelperContent();
		return content.SetContent(inner);
	}

	internal static IHtmlContent ContentChrome()
	{
		var chrome = "<p>Chrome</p>";
		return ContentBuilder(chrome);
	}
}
