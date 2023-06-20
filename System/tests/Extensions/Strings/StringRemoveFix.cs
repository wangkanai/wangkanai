// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable
using System.Globalization;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringRemoveFix
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";
	string? abcde  = "abcde";

	[Fact]
	public void RemovePreFixTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix("ab"));
	}

	[Fact]
	public void RemovePreFixNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix("abx"));
	}

	[Fact]
	public void RemovePreFixNoMatchIgnoreCaseTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix("ABx"));
	}

	[Fact]
	public void RemovePreFixIgnoreCaseTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}

	[Fact]
	public void RemovePreFixCultureTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseTest()
	{
		Assert.Equal("cde", abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchIgnoreCaseTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchIgnoreCaseNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchIgnoreCaseEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureNoMatchIgnoreCaseSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix(true, CultureInfo.InvariantCulture, "AB"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseNoMatchNullTest()
	{
		Assert.Equal(_null, _null.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseNoMatchEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseNoMatchSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePreFixCultureIgnoreCaseNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePreFix(true, CultureInfo.InvariantCulture, "ABx"));
	}
	
	[Fact]
	public void RemovePostFixTest()
	{
		Assert.Equal("abc", abcde.RemovePostFix("de"));
	}
	
	[Fact]
	public void RemovePostFixNullTest()
	{
		Assert.Equal(_null, _null.RemovePostFix("de"));
	}
	
	[Fact]
	public void RemovePostFixEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePostFix("de"));
	}
	
	[Fact]
	public void RemovePostFixSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePostFix("de"));
	}
	
	[Fact]
	public void RemovePostFixNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePostFix("dex"));
	}
	
	[Fact]
	public void RemovePostFixNoMatchIgnoreCaseTest()
	{
		Assert.Equal(abcde, abcde.RemovePostFix("dEx"));
	}
	
	[Fact]
	public void RemovePostFixIgnoreCaseTest()
	{
		Assert.Equal("abc", abcde.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureTest()
	{
		Assert.Equal("abc", abcde.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureIgnoreCaseTest()
	{
		Assert.Equal("abc", abcde.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchTest()
	{
		Assert.Equal(abcde, abcde.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchIgnoreCaseTest()
	{
		Assert.Equal(abcde, abcde.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNullTest()
	{
		Assert.Equal(_null, _null.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchNullTest()
	{
		Assert.Equal(_null, _null.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchIgnoreCaseNullTest()
	{
		Assert.Equal(_null, _null.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchIgnoreCaseEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureNoMatchIgnoreCaseSpaceTest()
	{
		Assert.Equal(_space, _space.RemovePostFix(true, CultureInfo.InvariantCulture, "DEx"));
	}
	
	[Fact]
	public void RemovePostFixCultureIgnoreCaseNullTest()
	{
		Assert.Equal(_null, _null.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
	
	[Fact]
	public void RemovePostFixCultureIgnoreCaseEmptyTest()
	{
		Assert.Equal(_empty, _empty.RemovePostFix(true, CultureInfo.InvariantCulture, "DE"));
	}
}