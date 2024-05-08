// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

// ReSharper disable UseCollectionExpression
namespace Wangkanai.Extensions;

/// <summary>
/// Provides extension methods for working with attributes.
/// </summary>
[DebuggerStepThrough]
public static class AttributeExtensions
{
	/// <summary>
	/// Splits the given string into an array of strings based on a specified separator.
	/// </summary>
	/// <param name="attribute">The Attribute object calling this method.</param>
	/// <param name="original">The original string to split.</param>
	/// <param name="separator">The separator used to split the string.</param>
	/// <returns>An array of strings after splitting the original string.</returns>
	public static string[] SplitString(this Attribute attribute, string original, char separator)
		=> string.IsNullOrEmpty(original)
			   ? Array.Empty<string>()
			   : original.Split(separator)
			             .Select(x => x.Trim())
			             .Where(x => !x.IsNullOrEmpty())
			             .ToArray();
}
