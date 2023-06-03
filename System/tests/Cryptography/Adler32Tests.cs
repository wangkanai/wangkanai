// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Cryptography;

public class Adler32Tests
{
	[Fact]
	public void ComputeChecksumThrowIfNull()
	{
		Assert.Throws<ArgumentNullException>(() => Adler32.ComputeChecksum(0, null!));
		Assert.Throws<ArgumentNullException>(() => Adler32.ComputeChecksum(0, null!, 0, 0));
	}
}