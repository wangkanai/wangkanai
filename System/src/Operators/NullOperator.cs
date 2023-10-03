// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Operators;

public interface INullOperator<T>
{
	bool HasValue(T         value);
	bool AddIfNotNull(ref T accumulator, T value);
}

public sealed class StructNullOperator<T> : INullOperator<T>
	where T : struct
{
	public bool HasValue(T value)
		=> true;

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		accumulator = Operator<T>.Add(accumulator, value);
		return true;
	}
}

public sealed class ClassNullOperator<T> : INullOperator<T>
	where T : class
{
	public bool HasValue(T value)
		=> value != null;

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		if (value == null)
			return false;

		accumulator = accumulator == null ? value : Operator<T>.Add(accumulator, value);
		return true;
	}
}