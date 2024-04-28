// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public class DictionaryByTypeTests
{
	private DictionaryByType subject;

	public DictionaryByTypeTests()
	{
		subject = new DictionaryByType();
	}

	[Fact]
	public void AddThenGet()
	{
		object o = new object();
		subject.Add("hi");
		subject.Add(10);
		subject.Add(o);

		Assert.Equal("hi", subject.Get<string>());
		Assert.Equal(10, subject.Get<int>());
		Assert.Same(o, subject.Get<object>());
	}

	[Fact]
	public void PutThenGet()
	{
		object o = new object();
		subject.Put("hi");
		subject.Put(10);
		subject.Put(o);

		Assert.Equal("hi", subject.Get<string>());
		Assert.Equal(10, subject.Get<int>());
		Assert.Same(o, subject.Get<object>());
	}

	[Fact]
	public void RepeatedAddForSameTypeThrowsException()
	{
		subject.Add("Hi");
		Assert.Throws<ArgumentException>(
			() => subject.Add("There")
		);
	}

	[Fact]
	public void RepeatedPutForSameTypeOverwritesValue()
	{
		subject.Put("Hi");
		Assert.Equal("Hi", subject.Get<string>());
		subject.Put("There");
		Assert.Equal("There", subject.Get<string>());
	}

	[Fact]
	public void GetFailsForMissingType()
	{
		Assert.Throws<KeyNotFoundException>(
			() => subject.Get<string>()
		);
	}

	[Fact]
	public void TryGetSucceedsForMissingType()
	{
		string x;
		Assert.False(subject.TryGet(out x));
		Assert.Null(x);
	}

	[Fact]
	public void TryGetFillsInValueForPresentType()
	{
		subject.Put("Hi");
		string x;
		Assert.True(subject.TryGet(out x));
		Assert.Equal("Hi", x);
	}
}