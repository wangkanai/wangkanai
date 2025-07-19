// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Models;

public class FederationResourceTests
{
	[Fact]
	public void DefaultInstance()
	{
		var resource = new FederationResource();
		Assert.NotNull(resource);
		Assert.Null(resource.Name);
		Assert.Null(resource.DisplayName);
		Assert.Null(resource.Description);

		Assert.True(resource.Enable);
		Assert.False(resource.Required);
		Assert.False(resource.Emphasize);
		Assert.True(resource.ShowInDiscoveryDocument);

		Assert.NotNull(resource.Claims);
		Assert.NotNull(resource.Properties);
	}
}
