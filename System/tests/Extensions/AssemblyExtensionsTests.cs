// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Extensions;

public class AssemblyExtensionsTests
{
	[Fact]
	public void GetVersion()
	{
		// Arrange
		var assembly = typeof(AssemblyExtensionsTests).Assembly;

		// Act
		var version = assembly.GetVersion();

		// Assert
		Assert.NotNull(version);
	}
}
