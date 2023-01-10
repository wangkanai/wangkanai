// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.Rendering;

internal readonly struct KeyedItemInfo
{
	public readonly int OldIndex;
	public readonly int NewIndex;
	public readonly int OldSiblingIndex;
	public readonly int NewSiblingIndex;

	public KeyedItemInfo(int oldIndex, int newIndex)
	{
		OldIndex        = oldIndex;
		NewIndex        = newIndex;
		OldSiblingIndex = -1;
		NewSiblingIndex = -1;
	}

	private KeyedItemInfo(in KeyedItemInfo copyFrom, int oldSiblingIndex, int newSiblingIndex)
	{
		this            = copyFrom;
		OldSiblingIndex = oldSiblingIndex;
		NewSiblingIndex = newSiblingIndex;
	}

	public KeyedItemInfo WithOldSiblingIndex(int oldSiblingIndex)
	{
		return new KeyedItemInfo(this, oldSiblingIndex, NewSiblingIndex);
	}

	public KeyedItemInfo WithNewSiblingIndex(int newSiblingIndex)
	{
		return new KeyedItemInfo(this, OldSiblingIndex, newSiblingIndex);
	}
}