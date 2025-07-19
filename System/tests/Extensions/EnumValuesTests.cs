// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
		var names = EnumValues<Fruit>.GetNames().ToList();
		Assert.Equal(4, names.Count);
		Assert.Contains(names, v => v == nameof(Fruit.Banana));
		Assert.Contains(names, v => v == nameof(Fruit.Apple));
		Assert.Contains(names, v => v == nameof(Fruit.Orange));
		Assert.Contains(names, v => v == nameof(Fruit.Pear));
	}

	[Fact]
	public void GetName()
	{
		var name = EnumValues<Fruit>.GetNameOriginal(Fruit.Apple);
		Assert.Equal(nameof(Fruit.Apple), name);
	}

	[Fact]
	public void GetNameFlags()
	{
		var names = EnumValues<Fruit>.GetNameOriginal(Fruit.Apple | Fruit.Orange);
		Assert.Equal($"{nameof(Fruit.Apple)},{nameof(Fruit.Orange)}", names);
	}
}

[Flags]
public enum Fruit
{
	Apple = 0,
	Orange = 1 << 0,
	Pear = 1 << 1,
	Banana = 1 << 2
}
