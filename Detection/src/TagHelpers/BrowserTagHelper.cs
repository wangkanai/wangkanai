// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;

using Wangkanai.Detection.Services;
using Wangkanai.Extensions;

namespace Microsoft.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = IncludeAttributeName)]
[HtmlTargetElement(ElementName, Attributes = ExcludeAttributeName)]
public class BrowserTagHelper : TagHelper
{
	private const string ElementName = "browser";
	private const string IncludeAttributeName = "include";
	private const string ExcludeAttributeName = "exclude";

	private readonly IBrowserService _resolver;

	private static readonly char[] NameSeparator = { ',' };

	public BrowserTagHelper(IBrowserService resolver)
	{
		_resolver = resolver.ThrowIfNull();
	}

	[HtmlAttributeName(IncludeAttributeName)]
	public string? Include { get; set; }

	[HtmlAttributeName(ExcludeAttributeName)]
	public string? Exclude { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		context.ThrowIfNull();
		output.ThrowIfNull();

		output.TagName = null;
		output.TagMode = TagMode.StartTagAndEndTag;

		if (string.IsNullOrEmpty(Include) && string.IsNullOrEmpty(Exclude))
			return;

		var browser = _resolver.Name.ToString();

		if (!string.IsNullOrWhiteSpace(Exclude))
		{
			var tokenizer = new StringTokenizer(Exclude, NameSeparator);
			foreach (var item in tokenizer)
			{
				var client = item.Trim();
				if (!client.HasValue || client.Length <= 0)
					continue;

				if (client.Equals(browser, StringComparison.OrdinalIgnoreCase))
				{
					output.SuppressOutput();
					return;
				}
			}
		}

		var has = false;
		if (!string.IsNullOrWhiteSpace(Include))
		{
			var tokenizer = new StringTokenizer(Include, NameSeparator);
			foreach (var item in tokenizer)
			{
				var client = item.Trim();
				if (client.HasValue && client.Length > 0)
				{
					has = true;
					if (client.Equals(browser, StringComparison.OrdinalIgnoreCase))
						return;
				}
			}
		}

		if (has)
			output.SuppressOutput();
	}
}
