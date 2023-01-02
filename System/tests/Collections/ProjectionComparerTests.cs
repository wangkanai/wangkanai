// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Collections;

public class ProjectionComparerTest
{
	static readonly NameAndNumber a10 = new("Aaaa", 5);
	static readonly NameAndNumber b15 = new("Bbbb", 15);

	[Fact]
	public void ProjectToStringWithIgnoredParameter()
	{
		IComparer<NameAndNumber> comparer = ProjectionComparer.Create(a10, x=> x.Name);
		TestComparisons(comparer);
	}

	private static void TestComparisons(IComparer<NameAndNumber> comparer)
	{
		Assert.Equal(0, comparer.Compare(a10, a10));
		Assert.Equal(0, comparer.Compare(b15, b15));
		Assert.Equal(0, comparer.Compare(null, null));
		
		Assert.True(comparer.Compare(null, a10) < 0);
		Assert.True(comparer.Compare(a10, null) > 0);
		
		Assert.True(comparer.Compare(a10, b15) < 0);
		Assert.True(comparer.Compare(b15, a10) > 0);
	}
}

record NameAndNumber(string Name, int Number);