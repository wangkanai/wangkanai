// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Wangkanai.Webmaster.TagHelpers;

[HtmlTargetElement(CanonicalAttributeName)]
public sealed class CanonicalTagHelper : TagHelper
{
	private const string CanonicalAttributeName = "seo-canonical";
	private const string HrefAttributeName = "href";
	private const string RelAttributeName = "rel";

	public override int Order => -1000;

	[HtmlAttributeNotBound]
	[ViewContext]
	public ViewContext ViewContext { get; set; }

	public string Site { get; set; }
	public string Path { get; set; }

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		context.ThrowIfNull();
		output.ThrowIfNull();

		output.TagName = "link";
		// output.Attributes.Clear();

		output.Attributes.Add(RelAttributeName, "canonical");

		if (string.IsNullOrEmpty(Path))
		{
			Path = ViewContext.HttpContext.Request.Path;
		}

		output.Attributes.Add(HrefAttributeName, Site + Path);
	}
}
