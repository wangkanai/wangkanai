// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;

using Xunit;

namespace Wangkanai.Extensions;

public class StringExtensionsTests
{
	#region Unicode

	[Fact]
	public void Unicode_Mixed()
	{
		Assert.True("sarinสาริน".IsUnicode());
	}

	[Fact]
	public void Unicode_AsciiOnly()
	{
		Assert.False("123".IsUnicode());
		Assert.False("abc".IsUnicode());
		Assert.False("ABC".IsUnicode());
	}

	[Fact]
	public void Unicode_UnicodeOnly()
	{
		Assert.True("สาริน".IsUnicode());
	}

	[Fact]
	public void Unicode_DigitOnly()
	{
		Assert.False("123".IsUnicode());
	}

	[Fact]
	public void Unicode_AlphabetOnly()
	{
		Assert.False("abc".IsUnicode());
		Assert.False("ABC".IsUnicode());
	}

	#endregion

	#region EnsureEndsWith

	[Fact]
	public void EnsureEndsWith_UseCases()
	{
		// Expected use-cases
		Assert.Equal("Test!", "Test".EnsureEndsWith('!'));
		Assert.Equal("Test!", "Test!".EnsureEndsWith('!'));

		Assert.Equal(@"c:\test\project\", @"c:\test\project".EnsureEndsWith('\\'));
		Assert.Equal(@"c:\test\project\", @"c:\test\project\".EnsureEndsWith('\\'));
	}

	[Fact]
	public void EnsureEndsWith_CaseDifference()
	{
		// Case differences
		Assert.Equal("TurkeYy", "TurkeY".EnsureEndsWith('y'));
		Assert.Equal("TurkeY", "TurkeY".EnsureEndsWith('y', StringComparison.OrdinalIgnoreCase));
	}

	[Fact]
	public void EnsureEndsWith_EdgeCaseForTurkish()
	{
		// Edge cases for Turkish 'i'
		Assert.Equal("TAKSİ", "TAKSİ".EnsureEndsWith('i', true, new CultureInfo("tr-TR")));
		Assert.Equal("TAKSİi", "TAKSİ".EnsureEndsWith('i', false, new CultureInfo("tr-TR")));
	}

	#endregion

	#region EnsureStartsWith

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

	#endregion
}