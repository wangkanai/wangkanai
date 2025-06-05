// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.Attributes;

public class ZeroIntegerAttributeTests
{
	[Fact]
	public void ZeroIntegerAttribute_Constructor()
	{
		var attribute = new ZeroIntegerAttribute();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Parameter_Attribute_Default()
	{
		var method    = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterDefault));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.Null(attribute);
	}
}

public class ZeroInteger
{
	public void ParameterDefault(int value) { }
}
