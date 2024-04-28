// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

using FluentAssertions;

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
		version.Should().NotBeNull();
		version.Should().Be(new Version(1, 2, 0, 0));
	}

	[Fact]
	public void GetVersion_Assembly()
	{
		// Arrange
		var assembly = Assembly.GetExecutingAssembly();

		// Act
		var version = assembly.GetVersion();

		// Assert
		version.Should().NotBeNull();
		version.Should().Be(new Version(1, 2, 0, 0));
	}

	[Fact]
	public void GetVersionString_Assembly()
	{
		// Arrange
		var assembly = typeof(AssemblyExtensionsTests).Assembly;

		// Act
		var version = assembly.GetVersionString();

		// Assert
		version.Should().NotBeNull();
		version.Should().Be("1.2.0.0");
	}

	[Fact]
	public void GetVersion_Type()
	{
		// Arrange
		var type = typeof(AssemblyExtensionsTests);

		// Act
		var version = type.GetVersion();

		// Assert
		version.Should().NotBeNull();
		version.Should().Be(new Version(1, 2, 0, 0));
	}

	[Fact]
	public void GetVersionString_Type()
	{
		// Arrange
		var type = typeof(AssemblyExtensionsTests);

		// Act
		var version = type.GetVersionString();

		// Assert
		version.Should().NotBeNull();
		version.Should().Be("1.2.0.0");
	}

	[Fact]
	public void GetInformationalVersion()
	{
		// Arrange
		var assembly = Assembly.GetExecutingAssembly();

		// Act
		var version = assembly.GetInformationalVersion();

		// Assert
		Assert.NotNull(version);
	}

	[Fact]
	public void GetFileVersion()
	{
		// Arrange
		var assembly = Assembly.GetExecutingAssembly();

		// Act
		var version = assembly.GetFileVersion();

		// Assert
		Assert.NotNull(version);
		version.Should().Be("1.3.0");
	}
}
