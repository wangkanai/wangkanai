// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

namespace Wangkanai.Domain;

public class AbstractTypeFactoryTests
{
	private GenericTypeInfo<Parent> _parentInfo = new(typeof(Parent));
	private GenericTypeInfo<Child> _childInfo = new(typeof(Child));

	[Fact]
	public void RegisterType_ThrowIfNull()
	{
		Assert.Throws<ArgumentNullException>(() => AbstractTypeFactory<Parent>.RegisterType(null));
		Assert.Throws<ArgumentNullException>(() => AbstractTypeFactory<Child>.RegisterType(null));
	}

	[Fact]
	public void RegisterType_Parent_Generic()
	{
		var result = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		RegisterType(_parentInfo, result);
	}

	[Fact]
	public void RegisterType_Parent_Method()
	{
		var result = AbstractTypeFactory<Parent>.RegisterType(typeof(Parent));
		RegisterType(_parentInfo, result);
	}

	[Fact]
	public void RegisterType_Child_Generic()
	{
		var result = AbstractTypeFactory<Child>.RegisterType<Child>();
		RegisterType(_childInfo, result);
	}

	[Fact]
	public void RegisterType_Child_Method()
	{
		var result = AbstractTypeFactory<Child>.RegisterType(typeof(Child));
		RegisterType(_childInfo, result);
	}

	[Fact]
	public void RegisterType_ParentChild()
	{
		var parent = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		var child = AbstractTypeFactory<Child>.RegisterType<Child>();
		Assert.NotEqual(parent.TypeName, child.TypeName);
		Assert.NotEqual(parent.Type, child.Type);
		Assert.Contains(parent.Type, child.GetAllSubclasses());
		Assert.DoesNotContain(child.Type, parent.GetAllSubclasses());
	}

	[Fact]
	public void OverrideType()
	{
		var parent = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		var child = AbstractTypeFactory<Child>.RegisterType<Child>();
		var result = AbstractTypeFactory<Parent>.OverrideType<Parent, Child>();
		Assert.Equal(child.Type, result.Type);
		Assert.Equal(child.TypeName, result.TypeName);
	}

	[Fact]
	public void TryCreateInstance()
	{
		var parent = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		var child = AbstractTypeFactory<Child>.RegisterType<Child>();
		var result = AbstractTypeFactory<Parent>.TryCreateInstance();
		Assert.NotNull(result);
		// Assert.IsType<Parent>(result);
	}

	[Fact]
	public void TryCreateInstance_Generic()
	{
		var parent = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		var child = AbstractTypeFactory<Child>.RegisterType<Child>();
		var result = AbstractTypeFactory<Parent>.TryCreateInstance<Parent>();
		Assert.NotNull(result);
		// Assert.IsType<Parent>(result);
	}


	[Fact]
	public void TryCreateInstance_TypeName()
	{
		var parent = AbstractTypeFactory<Parent>.RegisterType<Parent>();
		var child = AbstractTypeFactory<Child>.RegisterType<Child>();
		var result = AbstractTypeFactory<Parent>.TryCreateInstance("Parent");
		Assert.NotNull(result);
		// Assert.IsType<Parent>(result);
	}

	[Fact]
	public void FindTypeInfoByName_ShouldReturnTypeInfo_WhenTypeIsRegistered()
	{
		// Arrange
		AbstractTypeFactory<object>.RegisterType<string>();

		// Act
		var result = AbstractTypeFactory<object>.FindTypeInfoByName("String");

		// Assert
		Assert.Equal(typeof(string), result.Type);
	}

	[Fact]
	public void FindTypeInfoByName_ShouldReturnNull_WhenTypeIsNotRegistered()
	{
		// Arrange
		AbstractTypeFactory<object>.RegisterType<string>();

		// Act
		var result = AbstractTypeFactory<object>.FindTypeInfoByName("Int");

		// Assert
		Assert.Null(result);
	}

	private void RegisterType<T>(GenericTypeInfo<T> expected, GenericTypeInfo<T> actual)
	{
		Assert.Equal(expected.Type, actual.Type);
		Assert.Equal(expected.TypeName, actual.TypeName);
		Assert.Equal(expected.GetAllSubclasses().Count(), actual.GetAllSubclasses().Count());
	}
}

public class Parent
{
	public string? Name { get; set; }
}

public class Child : Parent
{
	public int Age { get; set; }
}
