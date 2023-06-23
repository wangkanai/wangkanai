// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Wangkanai.Exceptions;

using Xunit;

namespace Wangkanai.Extensions.Strings;

public class StringSlugTests
{
	string? _null  = null;
	string? _empty = string.Empty;
	string? _space = " ";

	[Fact]
	public void Normal()
	{
		Assert.Equal("hello-world", "hello world".GenerateSlug());
	}

	[Fact]
	public void Null()
	{
		Assert.Throws<ArgumentNullException>(() => _null.GenerateSlug());
	}

	[Fact]
	public void Empty()
	{
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.GenerateSlug());
	}

	[Fact]
	public void Space()
	{
		Assert.Equal(_empty, _space.GenerateSlug());
	}
	
	[Fact]
	public void Special()
	{
		Assert.Equal("hello-world", "hello world!@#$%^&*()_+{}|:\"<>?".GenerateSlug());
	}
	
	[Fact]
	public void Unicode()
	{
		Assert.Equal("hello-world", "hello world สวัสดี".GenerateSlug());
	}
	
	[Fact]
	public void Leading()
	{
		Assert.Equal("hello-world", " hello world".GenerateSlug());
	}
	
	[Fact]
	public void Trailing()
	{
		Assert.Equal("hello-world", "hello world ".GenerateSlug());
	}
	
	[Fact]
	public void Multiple()
	{
		Assert.Equal("hello-world-hello-world", "hello world  hello world".GenerateSlug());
	}
	
	[Fact]
	public void MultipleSpace()
	{
		Assert.Equal("hello-world-hello-world", "hello world   hello world".GenerateSlug());
	}
	
	[Fact]
	public void MultipleLeading()
	{
		Assert.Equal("hello-world-hello-world", "  hello world   hello world".GenerateSlug());
	}
	
	[Fact]
	public void Hyphen()
	{
		Assert.Equal("hello-world", "hello-world".GenerateSlug());
	}
	
	[Fact]
	public void Underscore()
	{
		Assert.Equal("helloworld", "hello_world".GenerateSlug());
	}
	
	[Fact]
	public void CamelCase()
	{
		//Assert.Equal("hello-world", "HelloWorld".GenerateSlug());
	}
}