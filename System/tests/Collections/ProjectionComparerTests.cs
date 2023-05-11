// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.System.Collections;

public class ProjectionComparerTest
{
	private static NameAndNumber A10 => ProjectionComparerExtensions.A10;
	private static NameAndNumber B15 => ProjectionComparerExtensions.B15;

	[Fact]
	public void ProjectToStringWithIgnoredParameter()
	{
		ProjectionComparer.Create(A10, x => x.Name)
		                  .AssertAll();
	}

	[Fact]
	public void ProjectToStringWithExplicitType()
	{
		ProjectionComparer.Create((NameAndNumber x) => x.Name)
		                  .AssertAll();
	}

	[Fact]
	public void ProjectToStringWithGenericType()
	{
		ProjectionComparer<NameAndNumber>.Create(x => x.Name)
		                                 .AssertAll();
	}

	[Fact]
	public void ProjectToNumberWithIgnoredParameter()
	{
		ProjectionComparer.Create(A10, x => x.Number)
		                  .AssertAll();
	}

	[Fact]
	public void ProjectToNumberWithExplicitType()
	{
		ProjectionComparer.Create((NameAndNumber x) => x.Number)
		                  .AssertAll();
	}

	[Fact]
	public void ProjectToNumberWithGenericType()
	{
		ProjectionComparer<NameAndNumber>.Create(x => x.Number)
		                                 .AssertAll();
	}

	[Fact]
	public void NullProjection()
	{
		Assert.Throws<ArgumentNullException>(
			() => new ProjectionComparer<NameAndNumber, string>(null)
		);
	}

	[Fact]
	public void ExplicitComparer()
	{
		var lowerA = new NameAndNumber("a", 10);
		var upperZ = new NameAndNumber("Z", 10);

		var ordinal     = new ProjectionComparer<NameAndNumber, string>(x => x.Name, StringComparer.Ordinal);
		var insensitive = new ProjectionComparer<NameAndNumber, string>(x => x.Name, StringComparer.OrdinalIgnoreCase);

		Assert.True(ordinal.Compare(lowerA, upperZ) > 0);
		Assert.True(insensitive.Compare(lowerA, upperZ) < 0);
	}
}

/// <summary>
/// Utility method extension to help perform appropriate assertions with the given comparer.
/// </summary>
internal static class ProjectionComparerExtensions
{
	public static readonly NameAndNumber A10 = new("Aaaa", 5);
	public static readonly NameAndNumber B15 = new("Bbbb", 15);

	public static void AssertAll(this IComparer<NameAndNumber> comparer)
	{
		Assert.Equal(0, comparer.Compare(A10, A10));
		Assert.Equal(0, comparer.Compare(B15, B15));
		Assert.Equal(0, comparer.Compare(null, null));

		Assert.True(comparer.Compare(null, A10) < 0);
		Assert.True(comparer.Compare(A10, null) > 0);

		Assert.True(comparer.Compare(A10, B15) < 0);
		Assert.True(comparer.Compare(B15, A10) > 0);
	}
}