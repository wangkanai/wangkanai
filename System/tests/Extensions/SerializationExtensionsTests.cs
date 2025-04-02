// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class SerializationExtensionsTests
{
	private readonly TestObject _source = new() { Name = "Test", Value = 1 };

	private readonly string _xml = $"<?xml version=\"1.0\" encoding=\"utf-16\"?>{Environment.NewLine}" +
	                               $"<TestObject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">{Environment.NewLine}" +
	                               $"  <Name>Test</Name>{Environment.NewLine}" +
	                               $"  <Value>1</Value>{Environment.NewLine}" +
	                               "</TestObject>";

	[Fact]
	public void SerializeXml()
	{
		// Arrange
		var source = _source;

		// Act
		var result = source.SerializeXml();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(_xml, result);
	}

	[Fact]
	public void DeserializeXml()
	{
		// Arrange
		var source = _source;

		// Act
		var result = _xml.DeserializeXml<TestObject>();

		// Assert
		Assert.NotNull(result);
		Assert.Equal(source.Name, result.Name);
		Assert.Equal(source.Value, result.Value);
	}
}

public class TestObject
{
	public string? Name  { get; set; }
	public int     Value { get; set; }
}
