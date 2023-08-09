// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Cryptography;

public class HashRandomTests
{
	[Fact]
	public void CreateRandomKey_Length32()
	{
		var hash = HashRandom.CreateRandomKey(32);
		Assert.NotNull(hash);
		Assert.NotEmpty(hash);
		Assert.Equal(32, hash.Length);
	}

	[Fact]
	public void CreateRandomKey_Length0()
	{
		Assert.Throws<ArgumentLessThanException>(() => HashRandom.CreateRandomKey(0));
	}

	[Fact]
	public void CreateRandomKey_LengthMinus()
	{
		Assert.Throws<ArgumentLessThanException>(() => HashRandom.CreateRandomKey(-1));
	}

	[Fact]
	public void CreateUniqueId_Length16_Hex()
	{
		var hash = HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Hex);
		Assert.NotNull(hash);
		Assert.NotEmpty(hash);
		Assert.Equal(32, hash.Length);
	}

	[Fact]
	public void CreateUniqueId_Length16_Base64()
	{
		var hash = HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Base64);
		Assert.NotNull(hash);
		Assert.NotEmpty(hash);
		Assert.Equal(24, hash.Length);
	}

	[Fact]
	public void CreateUniqueId_Length16_Base64Url()
	{
		var hash = HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Base64Url);
		Assert.NotNull(hash);
		Assert.NotEmpty(hash);
		Assert.Equal(22, hash.Length);
	}

	[Fact]
	public void CreateUniqueId_Length0()
	{
		Assert.Throws<ArgumentLessThanException>(() => HashRandom.CreateUniqueId(0));
	}

	[Fact]
	public void CreateUniqueId_LengthMinus()
	{
		Assert.Throws<ArgumentLessThanException>(() => HashRandom.CreateUniqueId(-1));
	}
	
	[Fact]
	public void CreateUniqueId_Length16_FormatInvalid()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => HashRandom.CreateUniqueId(16, (HashRandom.OutputFormat) 99));
	}
	
	[Fact]
	public void Next()
	{
		var hash = new HashRandom();
		var next = hash.Next();
		Assert.NotNull(next);
		Assert.NotEqual(0, next);
	}
	
	[Fact]
	public void Next_MaxValue()
	{
		var hash = new HashRandom();
		var next = hash.Next(10);
		Assert.NotNull(next);
		Assert.NotEqual(0, next);
		Assert.InRange(next, 0, 10);
	}
	
	[Fact]
	public void Next_MinValue_MaxValue()
	{
		var hash = new HashRandom();
		var next = hash.Next(10, 20);
		Assert.NotNull(next);
		Assert.NotEqual(0, next);
		Assert.InRange(next, 10, 20);
	}
	
	[Fact]
	public void Next_MinValue_MaxValue_MinValueMoreThanMaxValue()
	{
		var hash = new HashRandom();
		Assert.Throws<ArgumentMoreThanException>(() => hash.Next(20, 10));
	}
	
	[Fact]
	public void Next_MinValue_MaxValue_MinValueEqualsMaxValue()
	{
		var hash = new HashRandom();
		var next = hash.Next(10, 10);
		Assert.NotNull(next);
		Assert.NotEqual(0, next);
		Assert.Equal(10, next);
	}
	
	[Fact]
	public void NextBytes()
	{
		var hash = new HashRandom();
		var bytes = new byte[10];
		hash.NextBytes(bytes);
		Assert.NotNull(bytes);
		Assert.NotEmpty(bytes);
		Assert.Equal(10, bytes.Length);
	}
	
	[Fact]
	public void NextBytes_Null()
	{
		var hash = new HashRandom();
		Assert.Throws<ArgumentNullException>(() => hash.NextBytes(null!));
	}
	
	[Fact]
	public void NextBytes_Empty()
	{
		var hash = new HashRandom();
		var bytes = new byte[0];
		hash.NextBytes(bytes);
		Assert.NotNull(bytes);
		Assert.Empty(bytes);
		Assert.Equal(0, bytes.Length);
	}
}