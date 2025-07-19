// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public class ReverseComparerTests
{
	[Fact]
	public void NullComparer()
	{
		Assert.Throws<ArgumentNullException>(() => new ReverseComparer<int>(null));
	}

	[Fact]
	public void NormalComparer()
	{
		var original = StringComparer.Ordinal;
		var subject = new ReverseComparer<string>(original);

		Assert.Equal(original.Compare("x", "y"), subject.Compare("y", "x"));
		Assert.Equal(0, subject.Compare("x", "x"));
	}
}
