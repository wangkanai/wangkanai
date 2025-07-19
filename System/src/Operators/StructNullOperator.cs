// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Operators;

public sealed class StructNullOperator<T> : INullOperator<T>
	where T : struct
{
	public bool HasValue(T value) => true;

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		accumulator = Operator<T>.Add(accumulator, value);
		return true;
	}
}
