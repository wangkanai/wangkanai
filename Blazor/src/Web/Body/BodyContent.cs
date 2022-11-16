// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components;

using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components.Sections;

public sealed class BodyContent : BlazorComponentBase
{
    /// <summary>
    ///     Gets or sets the content to be rendered as the document title.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<SectionContent>(0);
        builder.AddAttribute(1, nameof(SectionContent.Name), BodyOutlet.CssClassOutletName);
        builder.AddAttribute(2, nameof(SectionContent.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}