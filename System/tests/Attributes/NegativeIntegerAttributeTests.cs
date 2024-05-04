// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.Attributes;

public class NegativeIntegerAttributeTests
{
	[Fact]
	public void NegativeIntegerAttribute_Constructor()
	{
		var attribute = new NegativeIntegerAttribute();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Parameter_Attribute_Default()
	{
		var method    = typeof(NegativeInteger).GetMethod(nameof(NegativeInteger.ParameterDefault));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<NegativeIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Parameter_Attribute_Exist()
	{
		var method    = typeof(NegativeInteger).GetMethod(nameof(NegativeInteger.ParameterExist));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<NegativeIntegerAttribute>();
		var expected  = "The value must be negative integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Parameter_Attribute_Error()
	{
		var method    = typeof(NegativeInteger).GetMethod(nameof(NegativeInteger.ParameterError));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<NegativeIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
	}
}

public class NegativeInteger
{
	public void ParameterDefault(int                                value) { }
	public void ParameterExist([NegativeInteger]                int value) { }
	public void ParameterError([NegativeInteger("error", true)] int value) { }
}
