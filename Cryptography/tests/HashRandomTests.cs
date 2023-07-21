// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;

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
		var hash = HashRandom.CreateUniqueId(16, HashRandom.OutputFormat.Hexadecimal);
		Assert.NotNull(hash);
		Assert.NotEmpty(hash);
		Assert.Equal(32, hash.Length);
	}
}