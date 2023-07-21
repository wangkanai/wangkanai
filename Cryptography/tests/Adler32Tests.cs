﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text;

using Wangkanai.Exceptions;

namespace Wangkanai.Cryptography;

public class Adler32Tests
{
	[Fact]
	public void Checksum_LessThanZero()
	{
		var data = Array.Empty<byte>();
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, data));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, data, -1, 0));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, data, 0, -1));
	}

	[Fact]
	public void Checksum_Zero()
	{
		var data = Array.Empty<byte>();
		Assert.Throws<ArgumentZeroException>(() => Adler32.ComputeChecksum(0, data, 0, 0));
	}

	[Fact]
	public void Checksum_Normal()
	{
		var data     = new byte[] { 0 };
		var checksum = Adler32.ComputeChecksum(0, data);
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
		var data = Array.Empty<byte>();
		Assert.Throws<ArgumentZeroException>(() => Adler32.ComputeChecksum(0, data, 0, 0));
	}
}