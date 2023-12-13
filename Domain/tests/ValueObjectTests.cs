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
}

public class Address(string street, string city, string state, string zip)
	: ValueObject
{
	public string Street { get; set; } = street;
	public string City   { get; set; } = city;
	public string State  { get; set; } = state;
	public string Zip    { get; set; } = zip;
}
