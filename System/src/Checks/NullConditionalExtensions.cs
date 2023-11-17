// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[DebuggerStepThrough]
public static class NullConditionalExtensions
{
	public static bool TrueIfNull<T>(this T? value) 
		=> value is null;

	public static bool FalseIfNull<T>(this T? value) 
		=> !value.TrueIfNull();
}
