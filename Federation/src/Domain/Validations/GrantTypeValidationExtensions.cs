// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Validations;

public static class GrantTypeValidationExtensions
{
	public static IEnumerable<string> ValidateGrantTypes(this IEnumerable<string> grantTypes)
	{
		grantTypes.ThrowIfNull();

		if (grantTypes.Any(type => type.Contains(' ')))
			throw new InvalidOperationException("Grant types cannot contain spaces");

		if (grantTypes.Count() == 1) return grantTypes;

		if (grantTypes.Count() != grantTypes.Distinct().Count())
			throw new InvalidOperationException("Grant types list cannot contain duplicate values");

		DisallowGrantTypeCombination(GrantType.Implicit, GrantType.AuthorizationCode, grantTypes);
		DisallowGrantTypeCombination(GrantType.Implicit, GrantType.Hybrid, grantTypes);
		DisallowGrantTypeCombination(GrantType.AuthorizationCode, GrantType.Hybrid, grantTypes);

		return grantTypes;
	}

	private static void DisallowGrantTypeCombination(string expected, string actual, IEnumerable<string> grantTypes)
	{
		if (grantTypes.Contains(expected, StringComparer.Ordinal) && grantTypes.Contains(actual, StringComparer.Ordinal))
			throw new InvalidOperationException($"Grant types list cannot contain both {expected} and {actual}");
	}
}