// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Helpers;

public static class IdGeneratorHelper
{
	public static string Generate(string prefix)
	{
		return prefix + Guid.NewGuid();
	}
}