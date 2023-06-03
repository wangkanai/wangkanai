// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Cryptography;

public class Adler32Tests
{
	[Fact]
	public void ComputeChecksumThrowIfNull()
	{
		Assert.Throws<ArgumentNullException>(() => Adler32.ComputeChecksum(0, null!));
	}

	public void ChecksumThrowIfLessThanZero()
	{
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0]));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0], -1, 0));
		Assert.Throws<ArgumentLessThanException>(() => Adler32.ComputeChecksum(-1, new byte[0], 0, -1));
	}
}