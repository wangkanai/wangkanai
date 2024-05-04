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
	public void Parameter_Attribute_Exist()
	{
		var method    = typeof(IntegerParameter).GetMethod(nameof(IntegerParameter.ZeroExit));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		var expected  = "The value must be zero integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Parameter_Attribute_Error()
	{
		var method    = typeof(IntegerParameter).GetMethod(nameof(IntegerParameter.ZeroError));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
	}
}
