// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Operators;

public interface INullOperator<T>
{
	bool HasValue(T value);

	bool AddIfNotNull(ref T accumulator, T value);
}
