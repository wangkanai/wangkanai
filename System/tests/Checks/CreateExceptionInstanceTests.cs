// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Checks;

public class CreateExceptionInstanceTests
{
	[Fact]
	public void CreateExceptionInstance()
	{
		var value     = 1;
		var exception = Check.CreateGenericExceptionInstance<ArgumentNullException>(nameof(value));
		Assert.Equal(1, value);
		Assert.Equal(typeof(ArgumentNullException), exception.GetType());
	}

	[Fact]
	public void CreateExceptionInstanceWithMessage()
	{
		var value     = 1;
		var exception = Check.CreateGenericExceptionInstance<ArgumentNullException>(nameof(value), "message");
		Assert.Equal(1, value);
		Assert.Equal(typeof(ArgumentNullException), exception.GetType());
		Assert.Equal("message (Parameter 'value')", exception.Message);
	}
}