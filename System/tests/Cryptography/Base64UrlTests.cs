// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text;

using Xunit;

namespace Wangkanai.Cryptography;

public class Base64UrlTests
{
	[Fact]
	public void Encode_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Base64Url.Encode(null!));
	}

	[Fact]
	public void Decode_Null()
	{
		Assert.Throws<ArgumentNullException>(() => Base64Url.Decode(null!));
	}

	[Fact]
	public void Decode_Invalid()
	{
		Assert.Throws<FormatException>(() => Base64Url.Decode("invalid"));
	}

	[Fact]
	public void Encode_Normal()
	{
		var bytes = "Hello World"u8.ToArray();
		var str   = Base64Url.Encode(bytes);
		Assert.Equal("SGVsbG8gV29ybGQ", str);
	}

	[Fact]
	public void Decode_Normal()
	{
		var bytes = "Hello World"u8.ToArray();
		var str   = Base64Url.Encode(bytes);
		var data  = Base64Url.Decode(str);
		Assert.Equal(bytes, data);
	}
}