// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
		Assert.NotNull(decorator.Instance);
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

	[Fact]
	public void ImplementationIsNotNull()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.NotNull(decorator.Instance);
	}

	[Fact]
	public void ImplementationIsEqual()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.Equal("test", decorator.Instance);
	}

	[Fact]
	public void ImplementationIsNotEqual()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.NotEqual("test1", decorator.Instance);
	}

	[Fact]
	public void ImplementationIsNotEqualWithNull()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.NotNull(decorator.Instance);
	}

	[Fact]
	public void ImplementationIsNotEqualWithNullString()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.NotEqual(string.Empty, decorator.Instance);
	}

	[Fact]
	public void ImplementationIsNotEqualWithNullObject()
	{
		var decorator = new Decorator<string, string>("test");
		Assert.NotEqual(new object(), decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNull()
	{
		var decorator = new DisposableDecorator<string>(null!);
		Assert.Null(decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNotNull()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.NotNull(decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsEqual()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.Equal("test", decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNotEqual()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.NotEqual("test1", decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNotEqualWithNull()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.NotNull(decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNotEqualWithNullString()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.NotEqual(string.Empty, decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsNotEqualWithNullObject()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.NotEqual(new object(), decorator.Instance);
	}

	[Fact]
	public void DisposableInstanceIsDisposable()
	{
		var decorator = new DisposableDecorator<string>("test");
		Assert.IsAssignableFrom<IDisposable>(decorator);
	}

	[Fact]
	public void DisposeExecuted()
	{
		var decorator = new DisposableDecorator<string>("test");
		decorator.Dispose();
		Assert.IsAssignableFrom<IDisposable>(decorator);
	}
}
