// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Primitives;
using Wangkanai.Detection.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(ElementName, Attributes = IncludeAttributeName)]
    [HtmlTargetElement(ElementName, Attributes = ExcludeAttributeName)]
    public class DeviceTagHelper : TagHelper
    {
        protected     IHtmlGenerator Generator { get; }
        private const string         ElementName          = "device";
        private const string         IncludeAttributeName = "include";
        private const string         ExcludeAttributeName = "exclude";

        private static readonly char[] NameSeparator = new[] {','};

        [HtmlAttributeName(IncludeAttributeName)]
        public string? Include { get; set; }

        [HtmlAttributeName(ExcludeAttributeName)]
        public string? Exclude { get; set; }

        private readonly IDeviceService _resolver;

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public DeviceTagHelper(IHtmlGenerator generator, IDeviceService resolver)
        {
            Generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = null;

            if (string.IsNullOrWhiteSpace(Include) && string.IsNullOrWhiteSpace(Exclude))
                return;

            var currentDeviceName = _resolver.Type.ToString();

            if (Exclude != null)
            {
                var tokenizer = new StringTokenizer(Exclude, NameSeparator);
                foreach (var item in tokenizer)
                {
                    var client = item.Trim();
                    if (client.HasValue && client.Length > 0)
                        if (client.Equals(currentDeviceName, StringComparison.OrdinalIgnoreCase))
                        {
                            output.SuppressOutput();
                            return;
                        }
                }
            }

            var hasDevice = false;
            if (Include != null)
            {
                var tokenizer = new StringTokenizer(Include, NameSeparator);
                foreach (var item in tokenizer)
                {
                    var client = item.Trim();
                    if (client.HasValue && client.Length > 0)
                    {
                        hasDevice = true;
                        if (client.Equals(currentDeviceName, StringComparison.OrdinalIgnoreCase))
                            return;
                    }
                }
            }

            if (hasDevice)
                output.SuppressOutput();
        }
    }
}