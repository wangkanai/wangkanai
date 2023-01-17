// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel;
using System.Runtime.Serialization;

using Xunit;

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
	public void ToStringInvariant()
	{
		var one = Country.Thailand;
		Assert.Equal("thailand", one.ToStringInvariant());
	}

	[Fact]
	public void ToStringInvariantFlag()
	{
		var flags = Country.Singapore;
		Assert.Equal("singapore", flags.ToStringInvariant());
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
		Assert.NotEqual("japan", japan);
	}
}

[Flags]
public enum Country
{
	[Description("ประเทศไทย")]
	Thailand = 0,

	[EnumMember(Value = "jp")]
	Japan = 1 << 0,
	Singapore = 1 << 1,
	Australia = 1 << 2
}