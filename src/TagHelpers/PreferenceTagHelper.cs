// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(ElementName, Attributes = OnlyAttributeName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PreferenceTagHelper : TagHelper
    {
        private const    string             ElementName       = "preference";
        private const    string             OnlyAttributeName = "only";
        protected        IHtmlGenerator     Generator { get; }
        private readonly IResponsiveService _responsive;
        private readonly IDeviceService     _device;

        [HtmlAttributeName(OnlyAttributeName)] public string? Only { get; set; }

        public PreferenceTagHelper(IHtmlGenerator generator, IResponsiveService responsive, IDeviceService device)
        {
            Generator   = generator ?? throw new ArgumentNullException(nameof(generator));
            _responsive = responsive ?? throw new ArgumentNullException(nameof(responsive));
            _device     = device ?? throw new ArgumentNullException(nameof(device));
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            if (output is null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = null;

            if (string.IsNullOrWhiteSpace(Only))
                return;

            if (_responsive.HasPreferred())
                return;

            if (DisplayOnlyDevice)
                return;

            output.SuppressOutput();
        }

        private bool DisplayOnlyDevice => _device.Type == OnlyDevice;

        private Device OnlyDevice => Enum.Parse<Device>(Only, true);
    }
}