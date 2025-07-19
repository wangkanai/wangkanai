// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
	public void Encode_Normal()
	{
		var bytes = "Hello World"u8.ToArray();
		var str = Base64Url.Encode(bytes);
		Assert.Equal("SGVsbG8gV29ybGQ", str);
	}

	[Fact]
	public void Decode_Normal()
	{
		var bytes = "Hello World"u8.ToArray();
		var str = Base64Url.Encode(bytes);
		var data = Base64Url.Decode(str);
		Assert.Equal(bytes, data);
	}

	[Fact]
	public void Encode_Decode()
	{
		var bytes = "Hello World"u8.ToArray();
		var str = Base64Url.Encode(bytes);
		var data = Base64Url.Decode(str);
		Assert.Equal(bytes, data);
	}

	[Fact]
	public void Encode_Decode_Url()
	{
		var bytes = "https://www.google.com"u8.ToArray();
		var str = Base64Url.Encode(bytes);
		var data = Base64Url.Decode(str);
		Assert.Equal(bytes, data);
	}

	[Fact]
	public void Encode_Decode_Url_With_Special_Characters()
	{
		var bytes = "https://www.google.com/?q=hello world"u8.ToArray();
		var str = Base64Url.Encode(bytes);
		var data = Base64Url.Decode(str);
		Assert.Equal(bytes, data);
	}
}
