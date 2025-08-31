// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Reflection;

using FluentAssertions;

namespace Wangkanai.Extensions;

public class AssemblyExtensionsTests
{
   [Fact]
   public void GetVersion_Assembly()
   {
      // Arrange
      var assembly = Assembly.GetExecutingAssembly();

      // Act
      var version = assembly.GetVersion();

      // Assert
      version.Should().NotBeNull();
      version.Should().NotBe(new(0, 0, 0, 0));
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
      version.Should().NotBe(new(0, 0, 0, 0));
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
}