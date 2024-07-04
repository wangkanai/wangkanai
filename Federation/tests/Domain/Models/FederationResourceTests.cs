// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Models;

public class FederationResourceTests
{
	[Fact]
	public void DefaultInstance()
	{
		var resource = new FederationResource();
		Assert.NotNull(resource);
		Assert.False(resource.Emphasize);
		Assert.NotNull(resource.Claims);
	}
}
