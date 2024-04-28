// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;

namespace Wangkanai.Extensions.Strings;

public class StringEnsureStartTests
{
	[Fact]
	public void EnsureStartsWith_UseCases()
	{
		// Expected use-cases
		Assert.Equal("~Test", "Test".EnsureStartsWith('~'));
		Assert.Equal("~Test", "~Test".EnsureStartsWith('~'));
	}

	[Fact]
	public void EnsureStartsWith_CaseDifference()
	{
		// Case differences
		Assert.Equal("tTurkey", "Turkey".EnsureStartsWith('t'));
		Assert.Equal("Turkey", "Turkey".EnsureStartsWith('t', StringComparison.OrdinalIgnoreCase));
	}

	[Fact]
	public void EnsureStartsWith_EdgeCaseForTurkish()
	{
		// Edge cases for Turkish 'i'
		Assert.Equal("İstanbul", "İstanbul".EnsureStartsWith('i', true, new CultureInfo("tr-TR")));
		Assert.Equal("iİstanbul", "İstanbul".EnsureStartsWith('i', false, new CultureInfo("tr-TR")));
	}
}