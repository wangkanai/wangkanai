// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Operators;

public sealed class ClassNullOperator<T> : INullOperator<T>
	where T : class
{
	public bool HasValue(T value) => value.FalseIfNull();

	public bool AddIfNotNull(ref T accumulator, T value)
	{
		if (value.TrueIfNull())
			return false;

		accumulator = accumulator == null ? value : Operator<T>.Add(accumulator, value);
		return true;
	}
}
