// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Components.Web;

public class BodyClass : ComponentBase
{
    [Inject] protected ILogger<BodyClass> Logger { get; set; }

    [Parameter] public string Default { get; set; } = default!;
    [Parameter] public string Add     { get; set; } = default!;
    [Parameter] public string Remove  { get; set; } = default!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        Logger.LogWarning("Start BodyClass build render tree");
        builder.OpenComponent<HtmlContent>(0);
        builder.AddAttribute(1, nameof(HtmlContent.Name), BodyOutlet.BodySectionOutletName);
        //builder.AddAttribute(2, BodyOutlet.CssClassOutletName, Add);
        //builder.AddAttribute(3, nameof(BodyContent.ChildContent), ChildContent);
        builder.CloseComponent();
    }
}