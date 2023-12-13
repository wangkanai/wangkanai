// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain.Tests;

public class ValueObjectTests
{
	[Fact]
	public void ValueObject_Equals_ShouldBeTrue()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		var value2 = new Address("123 Main St", "Redmond", "WA", "98052");
		Assert.True(value1.Equals(value2));
	}

	[Fact]
	public void ValueObject_Equals_ShouldBeFalse()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		var value2 = new Address("123 Main St", "Redmond", "WA", "98053");
		Assert.False(value1.Equals(value2));
	}

	[Fact]
	public void ValueObject_Equals_ShouldBeFalse_WhenNull()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		Assert.False(value1.Equals(null));
	}

	[Fact]
	public void ValueObject_Equals_ShouldBeFalse_WhenDifferentType()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		Assert.False(value1.Equals(new object()));
	}

	[Fact]
	public void ValueObject_GetHashCode_ShouldBeTrue()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		var value2 = new Address("123 Main St", "Redmond", "WA", "98052");
		Assert.Equal(value1.GetHashCode(), value2.GetHashCode());
	}

	[Fact]
	public void ValueObject_GetHashCode_ShouldBeFalse()
	{
		var value1 = new Address("123 Main St", "Redmond", "WA", "98052");
		var value2 = new Address("123 Main St", "Redmond", "WA", "98053");
		Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
	}
}

public class Address : ValueObject
{
	public Address() { }

	public Address(string street, string city, string state, string zip) : this()
	{
		Street = street;
		City   = city;
		State  = state;
		Zip    = zip;
	}

	public string Street { get; set; }
	public string City   { get; set; }
	public string State  { get; set; }
	public string Zip    { get; set; }
}
