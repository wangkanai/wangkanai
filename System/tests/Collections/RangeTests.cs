// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Collections;

public class RangeTests
{
	[Fact]
	public void CustomComparer()
	{
		var subject = new Range<string>("a", "f", StringComparer.Ordinal);
		Assert.False(subject.Contains("B"));
		Assert.True(subject.Contains("b"));

		subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase);
		Assert.True(subject.Contains("B"));
		Assert.True(subject.Contains("A"));
		Assert.True(subject.Contains("F"));
		Assert.False(subject.Contains("G"));
	}

	[Fact]
	public void CustomComparerExcludingEnd()
	{
		var subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase).ExcludeEnd();
		
		Assert.True(subject.Contains("A"));
		Assert.False(subject.Contains("F"));
	}

	[Fact]
	public void CustomComparerExcludingStart()
	{
		var subject = new Range<string>("a", "f", StringComparer.OrdinalIgnoreCase).ExcludeStart();
		
		Assert.True(subject.Contains("F"));
		Assert.False(subject.Contains("A"));
	}
	
	
}