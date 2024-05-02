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

	[Fact]
	public void Constructor_Default_WithNoAttribute()
	{
		var type      = typeof(IntegerConstructorDefault);
		var ctor      = type.GetConstructors();
		var attribute = ctor[0].GetCustomAttribute<PositiveIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Constructor_Attribute_Exist()
	{
		var type      = typeof(PositiveIntegerConstructorExist);
		var ctor      = type.GetConstructors();
		var attribute = ctor[0].GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "The value must be positive integer.";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
	}

	[Fact]
	public void Constructor_Attribute_Error()
	{
		var type      = typeof(PositiveIntegerConstructorError);
		var ctor      = type.GetConstructors();
		var attribute = ctor[0].GetCustomAttribute<PositiveIntegerAttribute>();
		var expected  = "error";
		Assert.NotNull(attribute);
		Assert.Equal(expected, attribute!.Message);
		Assert.True(attribute.IsError);
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
