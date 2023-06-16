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
}