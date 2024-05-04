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

	[Fact]
	public void Parameter_Attribute_Exist()
	{
		// var method    = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterExit));
		// var argument  = method!.GetParameters()[0];
		// var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		// var expected  = "The value must be zero integer.";
		// Assert.NotNull(attribute);
		// Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Parameter_Attribute_Message()
	{
		// var method    = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterMessage));
		// var argument  = method!.GetParameters()[0];
		// var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		// var expected  = "message";
		// Assert.NotNull(attribute);
		// Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Parameter_Attribute_Error()
	{
		// var method    = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterError));
		// var argument  = method!.GetParameters()[0];
		// var attribute = argument!.GetCustomAttribute<ZeroIntegerAttribute>();
		// var expected  = "error";
		// Assert.NotNull(attribute);
		// Assert.Equal(expected, attribute!.Message);
		// Assert.True(attribute.IsError);
	}
}

public class ZeroInteger
{
	public void ParameterDefault(int                            value) { }
	public void ParameterExit([ZeroInteger]                 int value) { }
	// public void ParameterMessage([ZeroInteger("message")] int value) { }
	// public void ParameterError([ZeroInteger("error", true)] int value) { }
}
