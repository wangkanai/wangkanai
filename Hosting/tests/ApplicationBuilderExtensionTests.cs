// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
