// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Collections;

namespace Wangkanai.Extensions;

public class ComparerExtensionsTests
{
	[Fact]
	public void DoubleReverseIsNoOperation()
	{
		var original = StringComparer.Ordinal;

		Assert.Same(original, original.Reverse().Reverse());
	}

	[Fact]
	public void SingleReverseReverse()
	{
		var original = StringComparer.Ordinal;
		var subject = original.Reverse();

		Assert.Equal(original.Compare("x", "y"), subject.Compare("y", "x"));
		Assert.Equal(0, subject.Compare("x", "x"));
	}

	[Fact]
	public void ThenByWithComparer()
	{
		var data = TestSample.Data;
		var primary = ProjectionComparer<TestSample>.Create(x => x.First);
		var secondary = ProjectionComparer<TestSample>.Create(x => x.Second);

		data.Sort(primary.ThenBy(secondary));

		Assert.True(new[] { 2, 10, 5 }.SequenceEqual(data.Select(x => x.Second)));
	}

	[Fact]
	public void ThenByWithProjection()
	{
		var data = TestSample.Data;
		var primary = ProjectionComparer<TestSample>.Create(x => x.First);

		data.Sort(primary.ThenBy(x => x.Second));

		Assert.True(new[] { 2, 10, 5 }.SequenceEqual(data.Select(x => x.Second)));
	}

	class TestSample
	{
		public int First { get; set; }
		public int Second { get; set; }

		public static List<TestSample> Data =>
			new List<TestSample>
			{
				new() { First = 1, Second = 10 },
				new() { First = 5, Second = 5 },
				new() { First = 1, Second = 2 }
			};
	}
}
