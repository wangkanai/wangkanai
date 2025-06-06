// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class DictionaryExtensionsTests
{
	[Fact]
	public void GetValueOrThrow_KeyExists_ReturnsCorrectValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.GetValueOrThrow("key", "Key not found");
		Assert.Equal(1, result);
	}

	[Fact]
	public void GetValueOrThrow_KeyDoesNotExist_ThrowsException()
	{
		var dictionary = new Dictionary<string, int>();
		Assert.Throws<KeyNotFoundException>(() => dictionary.GetValueOrThrow("key", "Key not found"));
	}

	[Fact]
	public void GetValueSafe_KeyExists_ReturnsCorrectValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.GetValueSafe("key");
		Assert.Equal(1, result);
	}

	[Fact]
	public void GetValueSafe_KeyDoesNotExist_ReturnsDefaultValue()
	{
		var dictionary = new Dictionary<string, int>();
		var result     = dictionary.GetValueSafe("key");
		Assert.Equal(0, result);
	}

	[Fact]
	public void TryGetValueSafe_KeyExists_ReturnsTrueAndCorrectValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.TryGetValueSafe("key", out var value);
		Assert.True(result);
		Assert.Equal(1, value);
	}

	[Fact]
	public void TryGetValueSafe_KeyDoesNotExist_ReturnsFalse()
	{
		var dictionary = new Dictionary<string, int>();
		var result     = dictionary.TryGetValueSafe("key", out var value);
		Assert.False(result);
		Assert.Equal(0, value);
		Assert.Equal(default, value);
	}

	[Fact]
	public void GetValueSafe_NullKey_ReturnsDefaultValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.GetValueSafe(null);
		Assert.Equal(0, result);
	}

	[Fact]
	public void GetValueSafe_WithCustomDefaultValue_KeyDoesNotExist_ReturnsCustomDefaultValue()
	{
		var dictionary = new Dictionary<string, int>();
		var result     = dictionary.GetValueSafe("key", 42);
		Assert.Equal(42, result);
	}

	[Fact]
	public void GetValueSafe_WithCustomDefaultValue_KeyExists_ReturnsActualValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.GetValueSafe("key", 42);
		Assert.Equal(1, result);
	}

	[Fact]
	public void GetValueSafe_WithCustomDefaultValue_NullKey_ReturnsCustomDefaultValue()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.GetValueSafe(null, 42);
		Assert.Equal(42, result);
	}

	[Fact]
	public void GetValueSafe_WithReferenceType_KeyExists_ReturnsCorrectValue()
	{
		var dictionary = new Dictionary<string, string> { { "key", "value" } };
		var result     = dictionary.GetValueSafe("key");
		Assert.Equal("value", result);
	}

	[Fact]
	public void GetValueSafe_WithReferenceType_KeyDoesNotExist_ReturnsNull()
	{
		var dictionary = new Dictionary<string, string>();
		var result     = dictionary.GetValueSafe("key");
		Assert.Null(result);
	}

	[Fact]
	public void TryGetValueSafe_NullKey_ReturnsFalse()
	{
		var dictionary = new Dictionary<string, int> { { "key", 1 } };
		var result     = dictionary.TryGetValueSafe(null, out var value);
		Assert.False(result);
		Assert.Equal(0, value);
	}

	[Fact]
	public void TryGetValueSafe_WithReferenceType_KeyExists_ReturnsTrueAndCorrectValue()
	{
		var dictionary = new Dictionary<string, string> { { "key", "value" } };
		var result     = dictionary.TryGetValueSafe("key", out var value);
		Assert.True(result);
		Assert.Equal("value", value);
	}

	[Fact]
	public void TryGetValueSafe_WithReferenceType_KeyDoesNotExist_ReturnsFalseAndNull()
	{
		var dictionary = new Dictionary<string, string>();
		var result     = dictionary.TryGetValueSafe("key", out var value);
		Assert.False(result);
		Assert.Null(value);
	}

	[Fact]
	public void TryGetValueSafe_WithNullableDictionary_ReturnsDefaultValue()
	{
		Dictionary<string, int> dictionary = null;
		Assert.Throws<NullReferenceException>(() => dictionary.TryGetValueSafe("key", out var value));
	}

	[Fact]
	public void GetValueSafe_WithDifferentKeyType_WorksCorrectly()
	{
		var dictionary = new Dictionary<int, string> { { 1, "one" } };
		var result     = dictionary.GetValueSafe(1);
		Assert.Equal("one", result);
		result = dictionary.GetValueSafe(2);
		Assert.Null(result);
	}

	[Fact]
	public void TryGetValueSafe_WithDifferentKeyType_WorksCorrectly()
	{
		var dictionary = new Dictionary<int, string> { { 1, "one" } };
		var result     = dictionary.TryGetValueSafe(1, out var value);
		Assert.True(result);
		Assert.Equal("one", value);

		result = dictionary.TryGetValueSafe(2, out value);
		Assert.False(result);
		Assert.Null(value);
	}
}
