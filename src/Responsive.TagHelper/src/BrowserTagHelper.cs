// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wangkanai.Responsive.TagHelpers
{
    [HtmlTargetElement("browser", TagStructure = TagStructure.WithoutEndTag)]
    public class BrowserTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            output.TagName = "div";
            output.Content.AppendHtml("browser");
        }
    }
}
