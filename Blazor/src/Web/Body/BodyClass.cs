// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components.Sections;

public class BodyClass : BlazorComponentBase
{
	[Inject] protected ILogger<BodyClass> Logger { get; set; }

	[Parameter] public string Default { get; set; } = default!;
	[Parameter] public string Add     { get; set; } = default!;
	[Parameter] public string Remove  { get; set; } = default!;

	[Parameter] public RenderFragment? ChildContent { get; set; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		try
		{
			Logger.LogInformation("Start BodyClass build render tree");
			builder.OpenComponent<SectionContent>(0);
			builder.AddAttribute(1, nameof(SectionContent.Name), BodyOutlet.BodySectionOutletName);
			builder.AddAttribute(2, BodyOutlet.CssClassOutletName, Add);
			builder.AddAttribute(3, nameof(BodyContent.ChildContent), ChildContent);
			builder.CloseComponent();
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, "Error BodyClass build render tree");
		}
	}
}