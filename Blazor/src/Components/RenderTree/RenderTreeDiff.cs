// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

public readonly struct RenderTreeDiff
{
	public readonly int                                 ComponentId;
	public readonly ArrayBuilderSegment<RenderTreeEdit> Edits;

	internal RenderTreeDiff(
		int                                 componentId,
		ArrayBuilderSegment<RenderTreeEdit> entries)
	{
		ComponentId = componentId;
		Edits       = entries;
	}
}