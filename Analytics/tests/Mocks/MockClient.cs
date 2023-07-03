// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Net.Http;

namespace Wangkanai.Analytics.Tests.Mocks;

public static class MockClient
{
	public static HttpRequestMessage RequestMessage(string url = "/")
	{
		var request = new HttpRequestMessage(HttpMethod.Get, url);
		request.Headers.Add("User-Agent", "analytics");
		return request;
	}
}