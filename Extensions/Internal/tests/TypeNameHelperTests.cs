// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.Internal.Tests;

public class TypeNameHelperTests
{
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName()
	{
		var type = typeof(TypeNameHelperTests);
		var result = TypeNameHelper.GetTypeDisplayName(type);
		Assert.Equal("TypeNameHelperTests", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName()
	{
		var type = typeof(TypeNameHelperTests);
		var result = TypeNameHelper.GetTypeDisplayName(type, true);
		Assert.Equal("Wangkanai.Extensions.Internal.Tests.TypeNameHelperTests", result);
	}
	
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames()
	{
		var type = typeof(TypeNameHelperTests);
		var result = TypeNameHelper.GetTypeDisplayName(type, true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.Tests.TypeNameHelperTests", result);
	}
}