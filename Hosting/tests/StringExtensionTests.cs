// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Hosting;

public class StringExtensionTests
{
	[Fact]
	public void UrlIsNull()
	{
		string url = null!;
		Assert.Equal("/", url.CleanUrlPath());
	}

	[Fact]
	public void UrlIsEmpty()
	{
		string url = string.Empty;
		Assert.Equal("/", url.CleanUrlPath());
	}

	[Fact]
	public void UrlIsWhiteSpace()
	{
		string url = " ";
		Assert.Equal("/", url.CleanUrlPath());
	}

	[Fact]
	public void UrlIsSlash()
	{
		string url = "/";
		Assert.Equal("/", url.CleanUrlPath());
	}

	[Fact]
	public void UrlIsSlashEnd()
	{
		string url = "/test/";
		Assert.Equal("/test", url.CleanUrlPath());
	}
}
