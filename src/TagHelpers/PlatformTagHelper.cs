// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;

using Wangkanai.Detection.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = IncludeAttributeName)]
[HtmlTargetElement(ElementName, Attributes = ExcludeAttributeName)]
public class PlatformTagHelper : TagHelper
{
    private readonly IPlatformService _resolver;
    private const    string           ElementName          = "platform";
    private const    string           IncludeAttributeName = "include";
    private const    string           ExcludeAttributeName = "exclude";

    private static readonly char[] NameSeparator = { ',' };

    [HtmlAttributeName(IncludeAttributeName)]
    public string? Include { get; set; }

    [HtmlAttributeName(ExcludeAttributeName)]
    public string? Exclude { get; set; }

    public PlatformTagHelper(IPlatformService resolver)
    {
        _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));
        if (output is null)
            throw new ArgumentNullException(nameof(output));

        output.TagName = null;

        if (string.IsNullOrEmpty(Include) && string.IsNullOrEmpty(Exclude))
            return;

        var platform = _resolver.Name.ToString();

        if (Exclude != null)
        {
            var tokenizer = new StringTokenizer(Exclude, NameSeparator);
            foreach (var item in tokenizer)
            {
                var client = item.Trim();
                if (!client.HasValue || client.Length <= 0)
                    continue;

                if (!client.Equals(platform, StringComparison.OrdinalIgnoreCase))
                    continue;

                output.SuppressOutput();
                return;
            }
        }

        var has = false;
        if (Include != null)
        {
            var tokenizer = new StringTokenizer(Include, NameSeparator);
            foreach (var item in tokenizer)
            {
                var client = item.Trim();
                if (client.HasValue && client.Length > 0)
                {
                    has = true;
                    if (client.Equals(platform, StringComparison.OrdinalIgnoreCase))
                        return;
                }
            }
        }

        if (has)
            output.SuppressOutput();
    }
}