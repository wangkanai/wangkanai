// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.Internal;

public class TypeNameHelperTests
{
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName();
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter_WithNestedTypeDelimiter()
	{
		var type   = typeof(DisplayNumeric);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsNull()
	{
		DisplayNumeric _null  = null!;
		var            result = _null.GetTypeDisplayName();
		Assert.Null(result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ObjectIsNull_WithFullName()
	{
		DisplayNumeric _null  = null!;
		var            result = _null.GetTypeDisplayName(true);
		Assert.Null(result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance()
	{
		var instance = new DisplayNumeric();
		var result   = instance.GetTypeDisplayName();
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance_WithFullName()
	{
		var instance = new DisplayNumeric();
		var result   = instance.GetTypeDisplayName(true);
		Assert.Equal("Wangkanai.Extensions.Internal.DisplayNumeric", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance_WithShortName()
	{
		var instance = new DisplayNumeric();
		var result   = instance.GetTypeDisplayName(false);
		Assert.Equal("DisplayNumeric", result);
	}
}

public class DisplayNumeric;