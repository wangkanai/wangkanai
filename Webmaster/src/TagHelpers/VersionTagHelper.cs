// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Wangkanai.Webmaster.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = FieldCountAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public class VersionTagHelper : TagHelper
{
    private const string ElementName = "version";
    private const string FieldCountAttributeName = "field";

    [HtmlAttributeName(FieldCountAttributeName)]
    public int FieldCount { get; set; } = 3;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        Check.NotNull(context);
        Check.NotNull(output);

        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        if (version is null)
        {
            output.SuppressOutput();
            return;
        }

        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.Append(version.ToString(FieldCount));
    }
}