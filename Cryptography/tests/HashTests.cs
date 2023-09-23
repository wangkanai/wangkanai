// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Cryptography;

public class HashTests
{
	[Fact]
	public void Md5_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Hash.HashMd5(null!));
	}
	
	[Fact]
	public void Sha256_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Hash.HashSha256(null!));
	}
	
	[Fact]
	public void She384_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Hash.HashSha384(null!));
	}
	
	[Fact]
	public void Sha512_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Hash.HashSha512(null!));
	}
}