// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

// ReSharper disable PreferConcreteValueOverDefault
// ReSharper disable SuggestVarOrType_BuiltInTypes
namespace Wangkanai.Checks;

public class NullEqualityTests
{
	#region EqualNull Tests

	[Fact]
	public void EqualNull_WithNullReferenceType_ReturnsTrue()
	{
		string value = null!;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonNullReferenceType_ReturnsFalse()
	{
		string value = "test";
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithEmptyString_ReturnsFalse()
	{
		string value = string.Empty;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithDefaultInt_ReturnsTrue()
	{
		int value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonDefaultInt_ReturnsFalse()
	{
		int value = 42;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNullableIntNull_ReturnsTrue()
	{
		int? value = null;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNullableIntZero_ReturnsFalse()
	{
		int? value = 0;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNullableIntNonZero_ReturnsFalse()
	{
		int? value = 42;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithDefaultGuid_ReturnsTrue()
	{
		Guid value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonDefaultGuid_ReturnsFalse()
	{
		Guid value = Guid.NewGuid();
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithDefaultDateTime_ReturnsTrue()
	{
		DateTime value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonDefaultDateTime_ReturnsFalse()
	{
		DateTime value = DateTime.Now;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithDefaultEnum_ReturnsTrue()
	{
		DayOfWeek value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonDefaultEnum_ReturnsFalse()
	{
		DayOfWeek value = DayOfWeek.Friday;
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithDefaultBool_ReturnsTrue()
	{
		bool value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithTrueBool_ReturnsFalse()
	{
		bool value = true;
		Assert.False(value.EqualNull());
	}

	#endregion

	#region NotNull Tests

	[Fact]
	public void NotNull_WithNullReferenceType_ReturnsFalse()
	{
		string value = null!;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonNullReferenceType_ReturnsTrue()
	{
		string value = "test";
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithEmptyString_ReturnsTrue()
	{
		string value = string.Empty;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithDefaultInt_ReturnsFalse()
	{
		int value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonDefaultInt_ReturnsTrue()
	{
		int value = 42;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNullableIntNull_ReturnsFalse()
	{
		int? value = null;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNullableIntZero_ReturnsTrue()
	{
		int? value = 0;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNullableIntNonZero_ReturnsTrue()
	{
		int? value = 42;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithDefaultGuid_ReturnsFalse()
	{
		Guid value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonDefaultGuid_ReturnsTrue()
	{
		Guid value = Guid.NewGuid();
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithDefaultDateTime_ReturnsFalse()
	{
		DateTime value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonDefaultDateTime_ReturnsTrue()
	{
		DateTime value = DateTime.Now;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithDefaultEnum_ReturnsFalse()
	{
		DayOfWeek value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonDefaultEnum_ReturnsTrue()
	{
		DayOfWeek value = DayOfWeek.Friday;
		Assert.True(value.NotNull());
	}

	[Fact]
	public void NotNull_WithDefaultBool_ReturnsFalse()
	{
		bool value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithTrueBool_ReturnsTrue()
	{
		bool value = true;
		Assert.True(value.NotNull());
	}

	#endregion

	#region Custom Types

	[Fact]
	public void EqualNull_WithNullCustomClass_ReturnsTrue()
	{
		TestClass value = null!;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonNullCustomClass_ReturnsFalse()
	{
		TestClass value = new TestClass();
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void NotNull_WithNullCustomClass_ReturnsFalse()
	{
		TestClass value = null!;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonNullCustomClass_ReturnsTrue()
	{
		TestClass value = new TestClass();
		Assert.True(value.NotNull());
	}

	[Fact]
	public void EqualNull_WithDefaultStruct_ReturnsTrue()
	{
		TestStruct value = default;
		Assert.True(value.EqualNull());
	}

	[Fact]
	public void EqualNull_WithNonDefaultStruct_ReturnsFalse()
	{
		TestStruct value = new TestStruct { Value = 42 };
		Assert.False(value.EqualNull());
	}

	[Fact]
	public void NotNull_WithDefaultStruct_ReturnsFalse()
	{
		TestStruct value = default;
		Assert.False(value.NotNull());
	}

	[Fact]
	public void NotNull_WithNonDefaultStruct_ReturnsTrue()
	{
		TestStruct value = new TestStruct { Value = 42 };
		Assert.True(value.NotNull());
	}

	#endregion

	private class TestClass { }

	private struct TestStruct
	{
		public int Value { get; set; }
	}
}
