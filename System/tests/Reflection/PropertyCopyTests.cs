// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

namespace Wangkanai.Reflection;

public class PropertyCopyTests
{
	[Fact] public void MissingProperty()              => Assert.Throws<ArgumentException>(() => PropertyCopy<Simple>.CopyFrom(new { Missing = "oh!" }));
	[Fact] public void ReadOnlyTargetPropertyThrows() => Assert.Throws<ArgumentException>(() => PropertyCopy<ReadOnly>.CopyFrom(new { Value = "oh!" }));
	[Fact] public void IncorrectTypeThrows()          => Assert.Throws<ArgumentException>(() => PropertyCopy<Simple>.CopyFrom(new { Value   = 10 }));

	[Fact]
	public void CopyToSameType()
	{
		var source = new Simple { Value = "test" };
		var target = PropertyCopy<Simple>.CopyFrom(source);
		Assert.Equal("test", target.Value);
	}

	[Fact]
	public void CopyToSimilarType()
	{
		var source = new Simple { Value = "test" };
		var target = PropertyCopy<Other>.CopyFrom(source);
		Assert.Equal("test", target.Value);
	}

	[Fact]
	public void CopyAnonymousType()
	{
		var target = PropertyCopy<Simple>.CopyFrom(new { Value = "anonymous" });
		Assert.Equal("anonymous", target.Value);
	}

	[Fact]
	public void CopyFromGenericType()
	{
		var source = new Generic<string> { Value = "generic" };
		var target = PropertyCopy<Simple>.CopyFrom(source);
		Assert.Equal("generic", target.Value);
	}

	[Fact]
	public void CopyToGenericType()
	{
		var source = new Simple { Value = "generic" };
		var target = PropertyCopy<Generic<string>>.CopyFrom(source);
		Assert.Equal("generic", target.Value);
	}
	[Fact]
	public void WriteOnlyTargetPropertyIgnored()
	{
		var source = new WriteOnly { Value = "copied", Write = "ignored" };
		var target = PropertyCopy<Simple>.CopyFrom(source);
		Assert.Equal("copied", target.Value);
	}

	[Fact] public void MultipleProperties()
	{
		var target = PropertyCopy<Three>.CopyFrom(new { Third = true, Second = 20, First = "multiple" });
		Assert.Equal("multiple", target.First);
		Assert.Equal(20, target.Second);
		Assert.True(target.Third);
	}

	[Fact]
	public void DerivedTypeIsAccepted()
	{
		var source = new Generic<Derived>() { Value = new Derived() };
		var target = PropertyCopy<Generic<Base>>.CopyFrom(source);
		Assert.Same(source.Value, target.Value);
	}

	[Fact]
	public void BaseTypeIsRejected()
	{
		var source = new Generic<Base> { Value = new Base() };
		Assert.Throws<ArgumentException>(() => PropertyCopy<Generic<Derived>>.CopyFrom(source));
	}

	class Base { }

	class Derived : Base { }

	class Simple
	{
		public string Value { get; set; }
	}

	class Other
	{
		public string Value { get; set; }
	}

	class ReadOnly
	{
		public string Value => "readonly";
	}

	class WriteOnly
	{
		public string WriteField;
		public string Write { set { WriteField = value; } }
		public string Value { get; set; }
	}

	class Generic<T>
	{
		public T Value { get; set; }
	}

	class Three
	{
		public string First  { get; set; }
		public int    Second { get; set; }
		public bool   Third  { get; set; }
	}
}
