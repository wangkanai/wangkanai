// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Hosting.Mocks;

namespace Wangkanai.Hosting;

public class ApplicationBuilderExtensionTests
{
	[Fact]
	public void ValidateOption_ThrowIfNull()
	{
		var app = MockServer.WebHostBuilder();
		//app.ValidateOption(new object());
	}
}
