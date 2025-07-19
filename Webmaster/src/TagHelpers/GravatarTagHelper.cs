// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Razor.TagHelpers;

using Wangkanai.Helpers;
using Wangkanai.Webmaster.Models;

namespace Wangkanai.Webmaster.TagHelpers;

[HtmlTargetElement(GravatarAttributeName, Attributes = EmailAttributeName, TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement(GravatarAttributeName, Attributes = SizeAttributeName, TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement(GravatarAttributeName, Attributes = RatingAttributeName, TagStructure = TagStructure.WithoutEndTag)]
[HtmlTargetElement(GravatarAttributeName, Attributes = ModeAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public sealed class GravatarTagHelper : TagHelper
{
	private const string GravatarAttributeName = "gravatar";
	private const string EmailAttributeName = "email";
	private const string SizeAttributeName = "size";
	private const string RatingAttributeName = "rating";
	private const string ModeAttributeName = "mode";

	public override int Order => -1000;

	[HtmlAttributeName(EmailAttributeName)]
	[EmailAddress]
	public string Email { get; set; }

	[HtmlAttributeName(SizeAttributeName)]
	public IconSize Size { get; set; }

	[HtmlAttributeName(RatingAttributeName)]
	public GravatarRating Rating { get; set; } = GravatarRating.g;

	[HtmlAttributeName(ModeAttributeName)]
	public GravatarMode Mode { get; set; } = GravatarMode.Default;

	public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
	{
		context.ThrowIfNull();
		output.ThrowIfNull();

		output.TagName = "img";
		var gravatar = new Gravatar(Email, Size, Rating);
		var src = new TagHelperAttribute("src", gravatar);
		output.Attributes.Add(src);

		return base.ProcessAsync(context, output);
	}

	private string GetMode(GravatarMode mode)
	{
		if (mode == GravatarMode.NotFound)
			return "404";
		return mode.ToString().ToLower();
	}
}
