using System;
using System.Reflection;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Microsoft.AspNetCore.Mvc.TagHelpers
{
    [HtmlTargetElement(ElementName, TagStructure = TagStructure.WithoutEndTag)]
    public class VersionTagHelper : TagHelper
    {
        private const string ElementName = "version";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            output.TagName = "span";
            output.TagMode = TagMode.StartTagAndEndTag;
            var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
            output.Content.Append(version);
        }
    }
}