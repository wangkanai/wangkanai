// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


// ReSharper disable InconsistentNaming

namespace Wangkanai.Checks;

public class CheckBooleanTests
{
	[Fact]
	public void CheckTrueIfNullThenReturnTrue()
	{
		byte? _byte = null;
		short? _short = null;
		int? _int = null;
		long? _long = null;
		float? _float = null;
		double? _double = null;
		decimal? _decimal = null;
		char? _char = null;
		string? _string = null;

		Assert.True(_byte.TrueIfNull());
		Assert.True(_short.TrueIfNull());
		Assert.True(_int.TrueIfNull());
		Assert.True(_long.TrueIfNull());
		Assert.True(_float.TrueIfNull());
		Assert.True(_double.TrueIfNull());
		Assert.True(_decimal.TrueIfNull());
		Assert.True(_char.TrueIfNull());
		Assert.True(_string.TrueIfNull());
	}

	[Fact]
	public void CheckTrueIfExistReturnFalse()
	{
		byte? _byte = 0;
		short? _short = 0;
		int? _int = 0;
		long? _long = 0;
		float? _float = 0;
		double? _double = 0.0;
		decimal? _decimal = 0;
		char _char = 'a';
		string _string = "xyz";

		Assert.False(_byte.TrueIfNull());
		Assert.False(_short.TrueIfNull());
		Assert.False(_int.TrueIfNull());
		Assert.False(_long.TrueIfNull());
		Assert.False(_float.TrueIfNull());
		Assert.False(_double.TrueIfNull());
		Assert.False(_decimal.TrueIfNull());
		Assert.False(_char.TrueIfNull());
		Assert.False(_string.TrueIfNull());
	}

	[Fact]
	public void CheckFalseIfNullThenReturnFalse()
	{
		byte? _byte = null;
		short? _short = null;
		int? _int = null;
		long? _long = null;
		float? _float = null;
		double? _double = null;
		decimal? _decimal = null;
		char? _char = null;
		string? _string = null;

		Assert.False(_byte.FalseIfNull());
		Assert.False(_short.FalseIfNull());
		Assert.False(_int.FalseIfNull());
		Assert.False(_long.FalseIfNull());
		Assert.False(_float.FalseIfNull());
		Assert.False(_double.FalseIfNull());
		Assert.False(_decimal.FalseIfNull());
		Assert.False(_char.FalseIfNull());
		Assert.False(_string.FalseIfNull());
	}

	[Fact]
	public void CheckFalseIfExistReturnFalse()
	{
		byte? _byte = 0;
		short? _short = 0;
		int? _int = 0;
		long? _long = 0;
		float? _float = 0;
		double? _double = 0.0;
		decimal? _decimal = 0;
		char? _char = 'a';
		string _string = "xyz";

		Assert.True(_byte.FalseIfNull());
		Assert.True(_short.FalseIfNull());
		Assert.True(_int.FalseIfNull());
		Assert.True(_long.FalseIfNull());
		Assert.True(_float.FalseIfNull());
		Assert.True(_double.FalseIfNull());
		Assert.True(_decimal.FalseIfNull());
		Assert.True(_char.FalseIfNull());
		Assert.True(_string.FalseIfNull());
	}
}
