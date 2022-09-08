// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.AspNetCore.Components.Web;

public sealed class BodyContent : ComponentBase
{
    /// <summary>
    /// Gets or sets the content to be rendered as the document title.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<SectionContent>(0);
        builder.AddAttribute(1, nameof(SectionContent.Name), BodyOutlet.CssClassOutletName);
        builder.AddAttribute(2, nameof(SectionContent.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}