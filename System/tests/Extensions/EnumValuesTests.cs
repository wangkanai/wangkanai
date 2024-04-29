// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class EnumValuesTests
{
	[Fact]
	public void GetFlags()
	{
		var one = Fruit.Apple;
		Assert.Single(one.GetFlags());
	}

	[Fact]
	public void GetValues()
	{
		var values = EnumValues<Fruit>.GetValues();
		Assert.Equal(4, values.Length);
	}

	[Fact]
	public void GetNames()
	{
		var names = EnumValues<Fruit>.GetNames();
		Assert.Equal(4, names.Count);
		names.ContainsValue(nameof(Fruit.Banana));
		names.ContainsValue(nameof(Fruit.Apple));
		names.ContainsValue(nameof(Fruit.Orange));
		names.ContainsValue(nameof(Fruit.Pear));
	}

	[Fact]
	public void GetName()
	{
		var name = EnumValues<Fruit>.GetName(Fruit.Apple);
		Assert.Equal(nameof(Fruit.Apple), name);
	}

	[Fact]
	public void GetNameFlags()
	{
		var names = EnumValues<Fruit>.GetName(Fruit.Apple | Fruit.Orange);
		Assert.Equal($"{nameof(Fruit.Apple)},{nameof(Fruit.Orange)}", names);
	}
}

[Flags]
public enum Fruit
{
	Apple  = 0,
	Orange = 1 << 0,
	Pear   = 1 << 1,
	Banana = 1 << 2
}
