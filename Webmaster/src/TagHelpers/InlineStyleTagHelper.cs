// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Caching.Memory;

namespace Wangkanai.Webmaster.TagHelpers;

[HtmlTargetElement(InlineStyleAttributeName, Attributes = HrefAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public sealed class InlineStyleTagHelper : InlineTagHelper
{
    private const string InlineStyleAttributeName = "inline-style";
    private const string HrefAttributeName = "href";

    [HtmlAttributeName(HrefAttributeName)]
    public string Href { get; set; }

    public InlineStyleTagHelper(IWebHostEnvironment webHostEnvironment, IMemoryCache cache, HtmlEncoder htmlEncoder, IUrlHelperFactory urlHelperFactory)
        : base(webHostEnvironment, cache, htmlEncoder, urlHelperFactory)
    {
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        Check.NotNull(context);
        Check.NotNull(output);

        var fileContent = await GetFileContentStringAsync(Href);
        if (fileContent is null)
        {
            output.SuppressOutput();
            return;
        }

        output.TagName = "style";
        output.Attributes.RemoveAll("href");
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.AppendHtml(fileContent);
    }
}