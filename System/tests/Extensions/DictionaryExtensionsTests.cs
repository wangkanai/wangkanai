// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.Tests;

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
}
