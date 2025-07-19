// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

#nullable enable

using System.Numerics;

namespace Wangkanai.Extensions;

public class TypeExtensionsTests
{
	private class Parent { }

	private class Child : Parent { }

	[Fact]
	public void SingleTypeInheritanceChainToParent()
	{
		var parent = typeof(Parent);
		var child = typeof(Child);
		var result = child.GetTypeInheritanceChainTo(parent);
		Assert.Single(result);
		Assert.Equal(typeof(Child), result[0]);
	}

	private class Animal { }

	private class Dog : Animal { }

	private class Husky : Dog { }

	[Fact]
	public void MultipleTypeInheritanceChainToParent()
	{
		var animal = typeof(Animal);
		var dog = typeof(Dog);
		var husky = typeof(Husky);

		var result = husky.GetTypeInheritanceChainTo(animal);
		Assert.Equal(2, result.Length);
		Assert.Equal(typeof(Husky), result[0]);
		Assert.Equal(typeof(Dog), result[1]);
	}

	private class Alone { }

	[Fact]
	public void SingleTypeInheritanceChainToSelf()
	{
		var alone = typeof(Alone);
		var result = alone.GetTypeInheritanceChainTo(alone);
		Assert.Single(result);
		Assert.Equal(typeof(Alone), result[0]);
	}

	[Fact]
	public void IntTypeInheritanceChainToNumeric()
	{
		var result = typeof(int).GetTypeInheritanceChainTo(typeof(INumber<>));
		Assert.Equal(2, result.Length);
		Assert.Equal(typeof(int), result[0]);
		Assert.Equal(typeof(ValueType), result[1]);
	}

	[Fact]
	public void IntTypeInheritanceChainToValueType()
	{
		var result = typeof(int).GetTypeInheritanceChainTo(typeof(ValueType));
		Assert.Single(result);
		Assert.Equal(typeof(int), result[0]);
	}

	[Fact]
	public void PrettyPrintAlone()
	{
		var alone = typeof(Alone);
		Assert.Equal("Alone", alone.PrettyPrint());
	}

	[Fact]
	public void PrettyPrintInt()
	{
		var alone = typeof(int);
		Assert.Equal("Int32", alone.PrettyPrint());
	}

	[Fact]
	public void PrettyPrintChain()
	{
		var parent = typeof(Parent);
		Assert.Equal("Parent", parent.PrettyPrint());
	}

	[Fact]
	public void PrettyPrintChainDepth()
	{
		var parent = typeof(Parent);
		Assert.Equal("Parent", parent.PrettyPrint(1));
	}

	[Fact]
	public void PrettyPrintRecursive()
	{
		var alone = typeof(Alone);
		Assert.Equal("Alone", alone.PrettyPrintRecursive(0));
	}

	[Fact]
	public void PrettyPrintRecursiveInt()
	{
		var alone = typeof(int);
		Assert.Equal("Int32", alone.PrettyPrintRecursive(0));
	}

	[Fact]
	public void PrettyPrintRecursiveIntDepth()
	{
		var alone = typeof(int);
		Assert.Equal("Int32", alone.PrettyPrintRecursive(1));
	}

	[Fact]
	public void PrettyPrintRecursiveChain()
	{
		var parent = typeof(Parent);
		var child = typeof(Child);
		Assert.Equal("Parent", parent.PrettyPrintRecursive(0));
		Assert.Equal("Child", child.PrettyPrintRecursive(0));
	}

	[Fact]
	public void PrettyPrintRecursiveChainDepth()
	{
		var parent = typeof(Parent);
		var child = typeof(Child);
		Assert.Equal("Parent", parent.PrettyPrintRecursive(1));
		Assert.Equal("Child", child.PrettyPrintRecursive(1));
	}

	[Fact]
	public void PrimitiveType()
	{
		Assert.True(typeof(int).IsPrimitive);
	}

	[Fact]
	public void NotPrimitiveType()
	{
		Assert.False(typeof(Alone).IsPrimitive);
	}

	[Fact]
	public void PrimitiveObject()
	{
		Assert.True(1.IsPrimitive());
	}

	[Fact]
	public void NotPrimitiveObjectNull()
	{
		Assert.True(((object?)null).IsPrimitive());
	}

	[Fact]
	public void NotPrimitiveObjectString()
	{
		Assert.True("".IsPrimitive());
	}

	[Fact]
	public void NotPrimitiveObject()
	{
		var alone = new Alone();
		Assert.False(alone.IsPrimitive());
	}

	[Fact]
	public void TypeIsNullable()
	{
		var type = typeof(int?);
		Assert.True(type.IsNullable());
	}

	[Fact]
	public void TypeIsNotNullable()
	{
		var type = typeof(int);
		Assert.False(type.IsNullable());
	}

	[Fact]
	public void TypeIsNullableObject()
	{
		var type = typeof(int?);
		Assert.True(type.IsNullable());
	}

	[Fact]
	public void MakeNullable()
	{
		var type = typeof(int);
		Assert.Equal(typeof(int?), type.MakeNullable());
	}

	[Fact]
	public void MakeNullableObject()
	{
		var type = typeof(int?);
		Assert.Equal(typeof(int?), type.MakeNullable());
	}

	[Fact]
	public void MakeNullableNullable()
	{
		var type = typeof(int?);
		Assert.Equal(typeof(int?), type.MakeNullable());
	}

	[Fact]
	public void MakeNullableFalse()
	{
		var type = typeof(int?);
		Assert.Equal(typeof(int), type.MakeNullable(false));
	}

	[Fact]
	public void MakeNullableGeneric()
	{
		var type = typeof(INumber<>);
		Assert.Equal(typeof(INumber<>), type.MakeNullable());
	}

	[Fact]
	public void MakeNullableGenericInt()
	{
		var type = typeof(INumber<int>);
		Assert.Equal(typeof(INumber<int>), type.MakeNullable());
	}

	[Fact]
	public void UnwrapNullable()
	{
		var type = typeof(int?);
		Assert.Equal(typeof(int), type.UnwrapNullable());
	}

	[Fact]
	public void UnwrapEnum()
	{
		Assert.Equal(typeof(int), typeof(TestEnum).UnwrapEnum());
	}

	[Fact]
	public void UnwrapEnumNullable()
	{
		Assert.Equal(typeof(int?), typeof(TestEnum?).UnwrapEnum());
	}
}

enum TestEnum
{
	One,
	Two
}
