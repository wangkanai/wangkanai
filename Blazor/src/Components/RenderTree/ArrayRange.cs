// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

public readonly struct ArrayRange<T>
{
	internal readonly T[] Array;
	internal readonly int Count;

	public ArrayRange(T[] array, int count)
	{
		Array = array;
		Count = count;
	}

	public ArrayRange<T> Clone()
	{
		var buffer = new T[Count];
		System.Array.Copy(Array, buffer, Count);
		return new ArrayRange<T>(buffer, Count);
	}
}