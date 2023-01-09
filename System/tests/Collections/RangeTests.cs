// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Cryptography.X509Certificates;

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

	[Fact]
	public void DefaultComparer()
	{
		var subject = new Range<int>(0, 5);

		Assert.True(subject.IncludesStart);
		Assert.True(subject.IncludesEnd);
		Assert.True(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.True(subject.Contains(5));
		Assert.False(subject.Contains(-1));
		Assert.False(subject.Contains(6));
	}
	
	[Fact]
	public void DefaultComparerExcludingEnd()
	{
		var subject = new Range<int>(0, 5).ExcludeEnd();

		Assert.True(subject.IncludesStart);
		Assert.False(subject.IncludesEnd);
		Assert.True(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.False(subject.Contains(5));
	}
	
	[Fact]
	public void DefaultComparerExcludingStart()
	{
		var subject = new Range<int>(0, 5).ExcludeStart();

		Assert.False(subject.IncludesStart);
		Assert.True(subject.IncludesEnd);
		Assert.False(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.True(subject.Contains(5));
	}

	[Fact]
	public void DefaultBothEnds()
	{
		var subject = new Range<int>(0, 5).ExcludeStart().ExcludeEnd();
		
		Assert.False(subject.IncludesStart);
		Assert.False(subject.IncludesEnd);
		Assert.False(subject.Contains(0));
		Assert.True(subject.Contains(1));
		Assert.False(subject.Contains(5));
	}

	[Fact]
	public void ExcludeThenIncludeStart()
	{
		var subject = new Range<int>(0, 5);
		subject = subject.ExcludeStart();
		Assert.False(subject.IncludesStart);
		subject = subject.IncludeStart();
		Assert.True(subject.IncludesStart);
	}
	
	[Fact]
	public void ExcludeThenIncludeEnd()
	{
		var subject = new Range<int>(0, 5);
		subject = subject.ExcludeEnd();
		Assert.False(subject.IncludesEnd);
		subject = subject.IncludeEnd();
		Assert.True(subject.IncludesEnd);
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}