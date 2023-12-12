// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfOutOfRangeExtension
{
	public static T ThrowIfOutOfRange<T>(this T index, [PositiveInteger] T lower, [PositiveInteger] T upper)
		where T : IBinaryInteger<T>
		=> index < lower || index >= upper
			   ? throw new ArgumentOutOfRangeException(nameof(index))
			   : index;
}
