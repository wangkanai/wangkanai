// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

namespace Wangkanai.Cryptography;

public class Adler32Tests
{
	[Fact]
	public void Checksum_LessThanZero()
	{
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0]));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0], -1, 0));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0], 0, -1));
	}

	[Fact]
	public void Checksum_Normal()
	{
		var checksum = Adler32.ComputeChecksum(0, new byte[0]);
		Assert.Equal(0, checksum);
	}

	[Fact]
	public void Checksum_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Adler32.ComputeChecksum(0, null!));
	}

	[Fact]
	public void Checksum_Empty()
	{
		//Assert.Throws<ArgumentZeroException>(() => Adler32.ComputeChecksum(0, new byte[0], 0, 0));
	}
}