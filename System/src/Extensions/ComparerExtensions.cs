// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Collections;

namespace Wangkanai.Extensions;

public static class ComparerExtensions
{
	public static IComparer<T> Reverse<T>(this IComparer<T> original)
	{
		if (original is ReverseComparer<T> reverse)
			return reverse.Original;
		
		return new ReverseComparer<T>(original);
	}
}