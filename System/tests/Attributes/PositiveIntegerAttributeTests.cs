// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.Attributes;

public class PositiveIntegerAttributeTests
{
	[Fact]
	public void PositiveIntegerAttribute_Constructor()
	{
		var attribute = new PositiveIntegerAttribute();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Method_Default_WithNoAttribute()
	{
		var method    = typeof(IntegerParameter).GetMethod(nameof(IntegerParameter.Default));
		var attribute = method!.GetCustomAttribute<PositiveIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Parameter_Attribute_Exist()
	{
		var method    = typeof(IntegerParameter).GetMethod(nameof(IntegerParameter.PositiveExist));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "The value must be positive integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Parameter_Attribute_Error()
	{
		var method    = typeof(IntegerParameter).GetMethod(nameof(IntegerParameter.PositiveError));
		var argument  = method!.GetParameters()[0];
		var attribute = argument!.GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
	}
}
