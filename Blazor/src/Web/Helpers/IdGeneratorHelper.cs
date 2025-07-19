// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Blazor.Helpers;

public static class IdGeneratorHelper
{
	public static string Generate(string prefix)
	{
		return prefix + Guid.NewGuid();
	}
}
