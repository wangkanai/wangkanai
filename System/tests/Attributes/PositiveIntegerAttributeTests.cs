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
	public void Struct_Default_WithNoAttribute()
	{
		var attribute = typeof(PositiveIntegerStructDefault).GetCustomAttribute<PositiveIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Struct_Attribute_Exist()
	{
		var attribute = typeof(PositiveIntegerStructExist).GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "The value must be positive integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Struct_Attribute_Error()
	{
		var attribute = typeof(PositiveIntegerStructError).GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
	}
}
