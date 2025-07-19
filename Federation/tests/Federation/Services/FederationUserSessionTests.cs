// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Services;

namespace Wangkanai.Internal.Services;

public class FederationUserSessionTests
{
	[Fact]
	public void CreateSessionIdCookieOptions_ReturnsExpected()
	{
		var session = new FederationUserSession();
		//var options = session.CreateSessionIdCookieOption();
		// Assert.NotNull(options);   
	}
}
