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
		var method = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterDefault));
		var argument = method!.GetParameters()[0];
		var attribute = argument.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Parameter_With_Attribute()
	{
		var method = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterWithAttribute));
		var argument = method!.GetParameters()[0];
		var attribute = argument.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Property_With_Attribute()
	{
		var property = typeof(ZeroInteger).GetProperty(nameof(ZeroInteger.PropertyWithAttribute));
		var attribute = property!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Property_Without_Attribute()
	{
		var property = typeof(ZeroInteger).GetProperty(nameof(ZeroInteger.PropertyWithoutAttribute));
		var attribute = property!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Field_With_Attribute()
	{
		var field = typeof(ZeroInteger).GetField("_fieldWithAttribute", BindingFlags.NonPublic | BindingFlags.Instance);
		var attribute = field!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Field_Without_Attribute()
	{
		var field = typeof(ZeroInteger).GetField("_fieldWithoutAttribute", BindingFlags.NonPublic | BindingFlags.Instance);
		var attribute = field!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.Null(attribute);
	}

	[Fact]
	public void Multiple_Parameters_With_Attribute()
	{
		var method = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.MultipleParametersWithAttribute));
		var parameters = method!.GetParameters();

		var firstAttribute = parameters[0].GetCustomAttribute<ZeroIntegerAttribute>();
		var secondAttribute = parameters[1].GetCustomAttribute<ZeroIntegerAttribute>();

		Assert.NotNull(firstAttribute);
		Assert.NotNull(secondAttribute);
	}

	[Fact]
	public void Attribute_Is_Not_Inherited()
	{
		var attributeType = typeof(ZeroIntegerAttribute);
		var usageAttribute = attributeType.GetCustomAttribute<AttributeUsageAttribute>();

		Assert.NotNull(usageAttribute);
		Assert.False(usageAttribute.Inherited);
	}

	[Fact]
	public void Derived_Class_Property_Does_Not_Inherit_Attribute()
	{
		var baseProperty = typeof(ZeroInteger).GetProperty(nameof(ZeroInteger.PropertyWithAttribute));
		var derivedProperty = typeof(DerivedZeroInteger).GetProperty(nameof(DerivedZeroInteger.PropertyWithAttribute));

		var baseAttribute = baseProperty!.GetCustomAttribute<ZeroIntegerAttribute>();
		var derivedAttribute = derivedProperty!.GetCustomAttribute<ZeroIntegerAttribute>();

		Assert.NotNull(baseAttribute);
		Assert.Null(derivedAttribute); // Attribute is not inherited
	}

	[Fact]
	public void Derived_Class_With_Explicit_Attribute()
	{
		var property = typeof(DerivedZeroInteger).GetProperty(nameof(DerivedZeroInteger.ExplicitAttributeProperty));
		var attribute = property!.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Derived_Class_Additional_Methods()
	{
		var method = typeof(DerivedZeroInteger).GetMethod(nameof(DerivedZeroInteger.DerivedMethod));
		var parameter = method!.GetParameters()[0];
		var attribute = parameter.GetCustomAttribute<ZeroIntegerAttribute>();
		Assert.NotNull(attribute);
	}

	[Fact]
	public void Derived_Class_Can_Override_Method_With_Attribute()
	{
		var baseMethod = typeof(ZeroInteger).GetMethod(nameof(ZeroInteger.ParameterWithAttribute));
		var derivedMethod = typeof(DerivedZeroInteger).GetMethod(nameof(DerivedZeroInteger.ParameterWithAttribute));

		var baseParameter = baseMethod!.GetParameters()[0];
		var derivedParameter = derivedMethod!.GetParameters()[0];

		var baseAttribute = baseParameter.GetCustomAttribute<ZeroIntegerAttribute>();
		var derivedAttribute = derivedParameter.GetCustomAttribute<ZeroIntegerAttribute>();

		Assert.NotNull(baseAttribute);
		Assert.NotNull(derivedAttribute); // New attribute is applied
	}
}

public class ZeroInteger
{
	public void ParameterDefault(int value) { }

	public void ParameterWithAttribute([ZeroInteger] int value) { }

	public void MultipleParametersWithAttribute([ZeroInteger] int first, [ZeroInteger] int second) { }

	[ZeroInteger]
	public int PropertyWithAttribute { get; set; }

	public int PropertyWithoutAttribute { get; set; }

	[ZeroInteger]
	private int _fieldWithAttribute = 0;

	private int _fieldWithoutAttribute = 0;
}

public class DerivedZeroInteger : ZeroInteger
{
	// This property will NOT inherit the attribute from the base class
	// since AttributeUsage.Inherited is false
	public new int PropertyWithAttribute { get; set; }

	// Explicitly applying the attribute in the derived class
	[ZeroInteger]
	public int ExplicitAttributeProperty { get; set; }

	// Method unique to derived class
	public void DerivedMethod([ZeroInteger] int value) { }

	// Overriding a method with attribute
	public new void ParameterWithAttribute([ZeroInteger] int value) { }
}
