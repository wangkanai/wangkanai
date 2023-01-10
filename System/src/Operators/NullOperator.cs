// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Operators;

internal interface INullOperator<T>
{
	bool HasValue(T         Value);
	bool AddIfNotNull(ref T accumulator, T value);
}

internal sealed class StructNullOperator<T> : INullOperator<T>
	where T : struct
{
	public bool HasValue(T value)
	{
		return true;
	}

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		accumulator = Operator<T>.Add(accumulator, value);
		return true;
	}
}

internal sealed class ClassNullOperator<T> : INullOperator<T>
	where T : class
{
	public bool HasValue(T value)
	{
		return value != null;
	}

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		if (value == null)
			return false;

		accumulator = accumulator == null ? value : Operator<T>.Add(accumulator, value);
		return true;
	}
}