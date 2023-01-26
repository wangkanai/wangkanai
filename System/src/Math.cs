// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public static partial class Math
{
	public static double Divider(double value, double divider)
		=> divider != 0 ? value / divider : 0;
	
	public static decimal Divider(decimal value, decimal divider)
		=> divider != 0 ? value / divider : 0;
}