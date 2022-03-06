// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;

using Wangkanai.Detection.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = IncludeAttributeName)]
[HtmlTargetElement(ElementName, Attributes = ExcludeAttributeName)]
public class EngineTagHelper : TagHelper
{
    private readonly IEngineService _resolver;
    private const    string         ElementName          = "engine";
    private const    string         IncludeAttributeName = "include";
    private const    string         ExcludeAttributeName = "exclude";

    private static readonly char[] NameSeparator = { ',' };

    [HtmlAttributeName(IncludeAttributeName)]
    public string? Include { get; set; }

    [HtmlAttributeName(ExcludeAttributeName)]
    public string? Exclude { get; set; }

    public EngineTagHelper(IEngineService resolver)
        => _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        Check.NotNull(context);
        Check.NotNull(output);

        output.TagName = null;

        if (string.IsNullOrEmpty(Include) && string.IsNullOrEmpty(Exclude))
            return;

        var engine = _resolver.Name.ToString();

        if (Exclude != null)
        {
            var tokenizer = new StringTokenizer(Exclude, NameSeparator);
            foreach (var item in tokenizer)
            {
                var client = item.Trim();
                if (!client.HasValue || client.Length <= 0)
                    continue;

                if (!client.Equals(engine, StringComparison.OrdinalIgnoreCase))
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
                    if (client.Equals(engine, StringComparison.OrdinalIgnoreCase))
                        return;
                }
            }
        }

        if (has)
            output.SuppressOutput();
    }
}