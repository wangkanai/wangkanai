// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;

using Wangkanai.Detection.Services;
using Wangkanai.Extensions;

namespace Microsoft.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = IncludeAttributeName)]
[HtmlTargetElement(ElementName, Attributes = ExcludeAttributeName)]
public class DeviceTagHelper : TagHelper
{
	private const string ElementName          = "device";
	private const string IncludeAttributeName = "include";
	private const string ExcludeAttributeName = "exclude";

	private static readonly char[] NameSeparator = { ',' };

	private readonly IDeviceService _resolver;

	public DeviceTagHelper(IDeviceService resolver)
	{
		_resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
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

		if (Include.IsNullOrEmpty() && Exclude.IsNullOrEmpty())
			return;

		var device = _resolver.Type.ToString();

		if (Exclude != null)
		{
			var tokenizer = new StringTokenizer(Exclude, NameSeparator);
			foreach (var item in tokenizer)
			{
				var client = item.Trim();
				if (client.HasValue && client.Length > 0)
					if (client.Equals(device, StringComparison.OrdinalIgnoreCase))
					{
						output.SuppressOutput();
						return;
					}
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
					if (client.Equals(device, StringComparison.OrdinalIgnoreCase))
						return;
				}
			}
		}

		if (has)
			output.SuppressOutput();
	}
}