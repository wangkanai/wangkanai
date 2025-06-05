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
		Assert.NotNull(attribute);
	}
}

public class NegativeInteger
{
	public void ParameterDefault(int                 value) { }
	public void ParameterExist([NegativeInteger] int value) { }
}
