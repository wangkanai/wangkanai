// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Rendering;

internal readonly struct RenderQueueEntry
{
	public readonly ComponentState ComponentState;
	public readonly RenderFragment RenderFragment;

	public RenderQueueEntry(ComponentState componentState, RenderFragment renderFragment)
	{
		ComponentState = componentState;
		RenderFragment = renderFragment ?? throw new ArgumentNullException(nameof(renderFragment));
	}
}