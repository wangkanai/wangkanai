// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using System.Numerics;

using Xunit;

namespace Wangkanai.Extensions;

public class TypeExtensionsTests
{
	private class Parent { }

	private class Child : Parent { }

	[Fact]
	public void SingleTypeInheritanceChainToParent()
	{
		var parent = typeof(Parent);
		var child  = typeof(Child);
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
		var dog    = typeof(Dog);
		var husky  = typeof(Husky);

		var result = husky.GetTypeInheritanceChainTo(animal);
		Assert.Equal(2, result.Length);
		Assert.Equal(typeof(Husky), result[0]);
		Assert.Equal(typeof(Dog), result[1]);
	}

	private class Alone { }

	[Fact]
	public void SingleTypeInheritanceChainToSelf()
	{
		var alone  = typeof(Alone);
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
	public void PrettyPrint()
	{
		var alone  = typeof(Alone);
		var result = alone.PrettyPrint();
		Assert.Equal("Alone", result);
	}
}