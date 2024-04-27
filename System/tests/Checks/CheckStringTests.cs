// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Exceptions;

using Xunit;
// ReSharper disable InconsistentNaming

namespace Wangkanai.Checks;

public class CheckStringTests
{
	[Fact]
	public void StringThrowIfNull()
	{
		char?   _char   = null;
		string? _string = null;

		Assert.Throws<ArgumentNullException>(() => _char.ThrowIfNull());
		Assert.Throws<ArgumentNullException>(() => _string.ThrowIfNull());

		Assert.Throws<CustomArgumentException>(() => _char.ThrowIfNull<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _string.ThrowIfNull<CustomArgumentException>());
	}

	[Fact]
	public void StringIsEmpty()
	{
		string? _null  = null;
		string  _empty = string.Empty;

		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty());
		Assert.Throws<ArgumentNullException>(() => _null.ThrowIfEmpty<ArgumentNullException>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfEmpty<CustomArgumentException>());

		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty());
		Assert.Throws<ArgumentEmptyException>(() => _empty.ThrowIfEmpty<ArgumentEmptyException>());
		Assert.Throws<CustomArgumentException>(() => _empty.ThrowIfEmpty<CustomArgumentException>());
	}

	[Fact]
	public void StringIsEmptyThrowException()
	{
		string? _null  = null;
		string  _empty = string.Empty;

		Assert.Throws<ArgumentException>(() => _null.ThrowIfEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _null.ThrowIfEmpty<ArgumentException>("Empty Exception"));
		Assert.Throws<ArgumentException>(() => _null.ThrowIfEmpty<ArgumentException>("Empty Exception", nameof(_null)));

		Assert.Throws<ArgumentException>(() => _empty.ThrowIfEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfEmpty<ArgumentException>("Empty Exception"));
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfEmpty<ArgumentException>("Empty Exception", nameof(_empty)));
	}

	[Fact]
	public void StringIsNotEmptyReturnValue()
	{
		var abc = "abc";

		Assert.Equal(abc, abc.ThrowIfEmpty());
		Assert.Equal(abc, abc.ThrowIfEmpty<ArgumentEmptyException>());
		Assert.Equal(abc, abc.ThrowIfEmpty<ArgumentEmptyException>("Empty Exception"));
		Assert.Equal(abc, abc.ThrowIfEmpty<ArgumentEmptyException>("Empty Exception", nameof(abc)));

		Assert.Equal(abc, abc.ThrowIfEmpty<CustomArgumentException>());
		Assert.Equal(abc, abc.ThrowIfEmpty<CustomArgumentException>("Empty Exception"));
		Assert.Equal(abc, abc.ThrowIfEmpty<CustomArgumentException>("Empty Exception", nameof(abc)));
	}

	[Fact]
	public void StringIsNullOrEmptyThrowNull()
	{
		string? _null  = null;
		string  _empty = string.Empty;

		Assert.Throws<ArgumentNullOrEmptyException>(() => _null.ThrowIfNullOrEmpty());
		Assert.Throws<ArgumentNullOrEmptyException>(() => _null.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNullOrEmpty<CustomArgumentException>());

		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.ThrowIfNullOrEmpty());
		Assert.Throws<ArgumentNullOrEmptyException>(() => _empty.ThrowIfNullOrEmpty<ArgumentNullOrEmptyException>());
		Assert.Throws<CustomArgumentException>(() => _empty.ThrowIfNullOrEmpty<CustomArgumentException>());
	}

	[Fact]
	public void StringIsNullOrEmptyThrowException()
	{
		string? _null  = null;
		string  _empty = string.Empty;

		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>("Null Exception"));
		Assert.Throws<ArgumentException>(() => _null.ThrowIfNullOrEmpty<ArgumentException>("Null Exception", nameof(_null)));

		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>());
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>("Null Exception"));
		Assert.Throws<ArgumentException>(() => _empty.ThrowIfNullOrEmpty<ArgumentException>("Null Exception", nameof(_null)));
	}

	[Fact]
	public void StringIsNotNullOrEmptyReturnValue()
	{
		string abc = "abc";

		Assert.Equal(abc, abc.ThrowIfNullOrEmpty());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty("Null Exception", nameof(abc)));

		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentNullException>());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<ArgumentNullException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomArgumentException>());
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomArgumentException>("Null Exception"));
		Assert.Equal(abc, abc.ThrowIfNullOrEmpty<CustomArgumentException>("Null Exception", nameof(abc)));
	}

	[Fact]
	public void StringIsNullOrWhiteSpaceThrowException()
	{
		string? _null  = null;
		string  _empty = string.Empty;
		string  _space = " ";


		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _null.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _null.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _null.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>());
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _null.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>("Null Exception"));
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNullOrWhitespace<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _null.ThrowIfNullOrWhitespace<CustomArgumentException>("Null Exception"));

		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _empty.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _empty.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _empty.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>());
		Assert.Throws<CustomArgumentException>(() => _empty.ThrowIfNullOrWhitespace<CustomArgumentException>());

		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _space.ThrowIfNullOrWhitespace());
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _space.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Throws<ArgumentNullOrWhitespaceException>(() => _space.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>());
		Assert.Throws<CustomArgumentException>(() => _space.ThrowIfNullOrWhitespace<CustomArgumentException>());
	}

	[Fact]
	public void StringIsNotNullOrWhitespaceThenReturn()
	{
		string _abc = "abc";

		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace("Null Exception"));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>("Null Exception"));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<ArgumentException>("Null Exception", nameof(_abc)));
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<CustomArgumentException>());
		Assert.Equal(_abc, _abc.ThrowIfNullOrWhitespace<CustomArgumentException>("Null Exception"));
	}

	[Fact]
	public void StringIsWhitespaceThrowException()
	{
		string _space     = " ";
		string _paragraph = new string(' ', 10);

		Assert.Throws<ArgumentWhitespaceException>(() => _space.ThrowIfWhitespace());
		Assert.Throws<ArgumentWhitespaceException>(() => _space.ThrowIfWhitespace("Whitespace Exception"));
		Assert.Throws<ArgumentWhitespaceException>(() => _space.ThrowIfWhitespace<ArgumentWhitespaceException>());
		Assert.Throws<ArgumentWhitespaceException>(() => _space.ThrowIfWhitespace<ArgumentWhitespaceException>("Whitespace Exception"));
		Assert.Throws<CustomArgumentException>(() => _space.ThrowIfWhitespace<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _space.ThrowIfWhitespace<CustomArgumentException>("Whitespace Exception"));

		Assert.Throws<ArgumentWhitespaceException>(() => _paragraph.ThrowIfWhitespace());
		Assert.Throws<ArgumentWhitespaceException>(() => _paragraph.ThrowIfWhitespace("Whitespace Exception"));
		Assert.Throws<ArgumentWhitespaceException>(() => _paragraph.ThrowIfWhitespace<ArgumentWhitespaceException>());
		Assert.Throws<ArgumentWhitespaceException>(() => _paragraph.ThrowIfWhitespace<ArgumentWhitespaceException>("Whitespace Exception"));
		Assert.Throws<CustomArgumentException>(() => _paragraph.ThrowIfWhitespace<CustomArgumentException>());
		Assert.Throws<CustomArgumentException>(() => _paragraph.ThrowIfWhitespace<CustomArgumentException>("Whitespace Exception"));
	}

	[Fact]
	public void StringIsNotWhitespaceThenReturn()
	{
		string abc = "abc";

		Assert.Equal(abc, abc.ThrowIfWhitespace());
		Assert.Equal(abc, abc.ThrowIfWhitespace("Whitespace Exception"));
		Assert.Equal(abc, abc.ThrowIfWhitespace<ArgumentWhitespaceException>());
		Assert.Equal(abc, abc.ThrowIfWhitespace<ArgumentException>());
		Assert.Equal(abc, abc.ThrowIfWhitespace<ArgumentException>("Whitespace Exception"));
		Assert.Equal(abc, abc.ThrowIfWhitespace<ArgumentException>("Whitespace Exception", nameof(abc)));
		Assert.Equal(abc, abc.ThrowIfWhitespace<CustomArgumentException>());
		Assert.Equal(abc, abc.ThrowIfWhitespace<CustomArgumentException>("Whitespace Exception"));
		Assert.Equal(abc, abc.ThrowIfWhitespace<CustomArgumentException>("Whitespace Exception", nameof(abc)));
	}
}
