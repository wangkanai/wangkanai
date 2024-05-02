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
	public void Struct_Default_WithNoAttribute()
	{
		var attribute = typeof(NegativeIntegerStructDefault).GetCustomAttribute<NegativeIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Struct_Attribute_Exist()
	{
		var attribute = typeof(NegativeIntegerStructExist).GetCustomAttribute<NegativeIntegerAttribute>();
		var expected  = "The value must be negative integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Struct_Attribute_Error()
	{
		var attribute = typeof(NegativeIntegerStructError).GetCustomAttribute<NegativeIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
	}
}
