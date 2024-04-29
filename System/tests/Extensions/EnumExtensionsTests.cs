// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel;
using System.Runtime.Serialization;

namespace Wangkanai.Extensions;

public class EnumExtensionsTests
{
	[Fact]
	public void GetFlag()
	{
		var one   = Country.Thailand;
		var two   = Country.Thailand | Country.Japan;
		var three = Country.Thailand | Country.Japan | Country.Singapore;
		var four  = Country.Thailand | Country.Japan | Country.Singapore | Country.Australia;
		Assert.Single(one.GetFlags());
		Assert.Equal(2, two.GetFlags().Count());
		Assert.Equal(3, three.GetFlags().Count());
		Assert.Equal(4, four.GetFlags().Count());
	}

	[Fact]
	public void ToLowerString_FlagSingle()
	{
		var flags = Country.Singapore;
		Assert.Equal("singapore", flags.ToLowerString());
	}

	[Fact]
	public void ToLowerString_FlagTwo()
	{
		var flags = Country.Thailand | Country.Singapore;
		Assert.Equal("thailand,singapore", flags.ToLowerString());
	}

	[Fact]
	public void ToUpperString_FlagSingle()
	{
		var flags = Country.Singapore;
		Assert.Equal("SINGAPORE", flags.ToUpperString());
	}

	[Fact]
	public void ToUpperString_FlagTwo()
	{
		var flags = Country.Thailand | Country.Singapore;
		Assert.Equal("THAILAND,SINGAPORE", flags.ToUpperString());
	}

	[Fact]
	public void ToOriginalString_FlagSingle()
	{
		var flags = Country.Singapore;
		Assert.Equal("Singapore", flags.ToOriginalString());
	}

	[Fact]
	public void ToOriginalString_FlagTwo()
	{
		var flags = Country.Thailand | Country.Singapore;
		Assert.Equal("Thailand,Singapore", flags.ToOriginalString());
	}

	[Fact]
	public void EnumItemDescription()
	{
		var thailand = Country.Thailand.GetDescription();
		Assert.Equal("ประเทศไทย", thailand);
		Assert.NotEqual("Thailand", thailand);
	}

	[Fact]
	public void EnumItemMemberValue()
	{
		var japan = Country.Japan.GetMemberValue();
		Assert.Equal("jp", japan);
		Assert.NotEqual("Japan", japan);
	}

	[Fact]
	public void ContainsOriginal_NameSingle_FlagSingle()
	{
		var name = "Japan";
		var flags = Country.Japan;
		Assert.True(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameSingle_FlagTwo()
	{
		var name = "Japan";
		var flags = Country.Japan | Country.Singapore;
		Assert.True(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameTwo_FlagSingle()
	{
		var name = "Thailand,Singapore";
		var flags = Country.Singapore;
		Assert.True(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameTwo_FlagTwo()
	{
		var name = "Thailand,Singapore";
		var flags = Country.Thailand | Country.Singapore;
		Assert.True(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameSingle_NotIn_FlagSingle()
	{
		var name = "Thailand";
		var flags = Country.Singapore;
		Assert.False(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameSingle_NotIn_FlagTwo()
	{
		var name = "Thailand";
		var flags = Country.Japan | Country.Singapore;
		Assert.False(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameTwo_NotIn_FlagSingle()
	{
		var name = "Thailand,Singapore";
		var flags = Country.Japan;
		Assert.False(name.ContainsOriginal(flags));
	}

	[Fact]
	public void ContainsOriginal_NameTwo_NotIn_FlagTwo()
	{
		var name = "Thailand,Singapore";
		var flags = Country.Australia | Country.Japan;
		Assert.False(name.ContainsOriginal(flags));
	}
}

[Flags]
public enum Country
{
	[Description("ประเทศไทย")] Thailand  = 1 << 0,
	[EnumMember(Value = "jp")] Japan     = 1 << 1,
	[EnumMember(Value = "sg")] Singapore = 1 << 2,
	[EnumMember(Value = "au")] Australia = 1 << 3
}
