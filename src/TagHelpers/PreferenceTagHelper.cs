using System;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Wangkanai.Detection.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(ElementName, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PreferenceTagHelper : TagHelper
    {
        private const string ElementName = "preference";

        protected        IHtmlGenerator     Generator { get; }
        private readonly IPreferenceService _preference;

        public PreferenceTagHelper(IHtmlGenerator generator, IPreferenceService preference)
        {
            Generator   = generator;
            _preference = preference;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = null;

            if (!_preference.IsSet) 
                output.SuppressOutput();
        }
    }
}