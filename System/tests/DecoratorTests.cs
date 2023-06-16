// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai;

public class DecoratorTests
{
	[Fact]
	public void InstanceIsNull()
	{
		var decorator = new Decorator<string>(null!);
		Assert.Null(decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsNotNull()
	{
		var decorator = new Decorator<string>("test");
		Assert.NotNull(decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsEqual()
	{
		var decorator = new Decorator<string>("test");
		Assert.Equal("test", decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsNotEqual()
	{
		var decorator = new Decorator<string>("test");
		Assert.NotEqual("test1", decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsNotEqualWithNull()
	{
		var decorator = new Decorator<string>("test");
		Assert.NotEqual(null, decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsNotEqualWithNullString()
	{
		var decorator = new Decorator<string>("test");
		Assert.NotEqual(string.Empty, decorator.Instance);
	}
	
	[Fact]
	public void InstanceIsNotEqualWithNullObject()
	{
		var decorator = new Decorator<string>("test");
		Assert.NotEqual(new object(), decorator.Instance);
	}

	[Fact]
	public void ImplementationIsNull()
	{
		var decorator = new Decorator<string, string>(null!);
		Assert.Null(decorator.Instance);
	}
}