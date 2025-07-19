// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Collections;

public class ProjectionEqualityComparerTests
{
	private static NameAndNumber A10 => ProjectionEqualityComparerExtensions.A10;
	private static NameAndNumber A15 => ProjectionEqualityComparerExtensions.A15;
	private static NameAndNumber B10 => ProjectionEqualityComparerExtensions.B10;
	private static NameAndNumber B15 => ProjectionEqualityComparerExtensions.B15;

	[Fact]
	public void ProjectionToStringWithExplicitType()
	{
		ProjectionEqualityComparer.Create((NameAndNumber x) => x.Name).AssertNameComparison();
	}

	[Fact]
	public void ProjectionToNumberWithExplicitType()
	{
		ProjectionEqualityComparer.Create((NameAndNumber x) => x.Number).AssertNumberComparison();
	}

	[Fact]
	public void ProjectionToStringWithIgnored()
	{
		ProjectionEqualityComparer.Create(x => x.Name, A10).AssertNameComparison();
	}

	[Fact]
	public void ProjectionToNumberWithIgnored()
	{
		ProjectionEqualityComparer.Create(x => x.Number, A10).AssertNumberComparison();
	}

	[Fact]
	public void ProjectionToStringWithGenericType()
	{
		ProjectionEqualityComparer<NameAndNumber>.Create(x => x.Name).AssertNameComparison();
	}

	[Fact]
	public void ProjectionToNumberWithGenericType()
	{
		ProjectionEqualityComparer<NameAndNumber>.Create(x => x.Number).AssertNumberComparison();
	}

	[Fact]
	public void NullProjection()
	{
		Assert.Throws<ArgumentNullException>(() => new ProjectionEqualityComparer<NameAndNumber, string>(null));
	}

	[Fact]
	public void ExplicitComparer()
	{
		var lowerA = new NameAndNumber("a", 10);
		var upperA = new NameAndNumber("A", 10);

		var ordinal = new ProjectionEqualityComparer<NameAndNumber, string>(x => x.Name, StringComparer.Ordinal);
		var insensitive = new ProjectionEqualityComparer<NameAndNumber, string>(x => x.Name, StringComparer.OrdinalIgnoreCase);

		ordinal.AssertNotEqual(lowerA, upperA);
		insensitive.AssertEqual(lowerA, upperA);
	}
}

/// <summary>
/// Utility method extension to help perform appropriate assertions with the given comparer.
/// </summary>
internal static class ProjectionEqualityComparerExtensions
{
	public static readonly NameAndNumber A10 = new("Aaaa", 10);
	public static readonly NameAndNumber A15 = new("Aaaa", 15);
	public static readonly NameAndNumber B10 = new("Bbbb", 10);
	public static readonly NameAndNumber B15 = new("Bbbb", 15);


	public static void AssertEqual(this IEqualityComparer<NameAndNumber> comparer, NameAndNumber x, NameAndNumber y)
	{
		Assert.True(comparer.Equals(x, y));
		Assert.True(comparer.Equals(y, x));

		if (x != null && y != null)
			Assert.Equal(comparer.GetHashCode(x), comparer.GetHashCode(y));
	}

	public static void AssertNotEqual(this IEqualityComparer<NameAndNumber> comparer, NameAndNumber x, NameAndNumber y)
	{
		Assert.False(comparer.Equals(x, y));
		Assert.False(comparer.Equals(y, x));

		if (x != null && y != null)
			Assert.NotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
	}

	public static void AssertNameComparison(this IEqualityComparer<NameAndNumber> comparer)
	{
		comparer.AssertBasic();
		comparer.AssertEqual(A10, A15);
		comparer.AssertEqual(B10, B15);
		comparer.AssertNotEqual(A10, B10);
		comparer.AssertNotEqual(A15, B15);
	}

	public static void AssertNumberComparison(this IEqualityComparer<NameAndNumber> comparer)
	{
		comparer.AssertBasic();
		comparer.AssertEqual(A10, B10);
		comparer.AssertEqual(A15, B15);
		comparer.AssertNotEqual(A10, A15);
		comparer.AssertNotEqual(B10, B15);
	}

	private static void AssertBasic(this IEqualityComparer<NameAndNumber> comparer)
	{
		comparer.AssertEqual(A10, A10);
		comparer.AssertEqual(B15, B15);
		comparer.AssertEqual(null, null);
		comparer.AssertNotEqual(A10, B15);
		comparer.AssertNotEqual(A10, null);
		comparer.AssertNotEqual(B15, null);
		comparer.AssertNotEqual(null, A10);
		comparer.AssertNotEqual(null, B15);

		Assert.Throws<ArgumentNullException>(() => comparer.GetHashCode(null));
	}
}
