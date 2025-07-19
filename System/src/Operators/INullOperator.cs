// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Operators;

public interface INullOperator<T>
{
	bool HasValue(T value);

	bool AddIfNotNull(ref T accumulator, T value);
}
