// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Mocks;

internal static class MockClient
{
	public static HttpRequestMessage CreateRequest(Device agent, string url = "/")
		=> CreateRequest(agent.ToString(), url);

	public static HttpRequestMessage CreateRequest(string agent, string url = "/")
	{
		var request = new HttpRequestMessage(HttpMethod.Get, url);
		request.Headers.Add("User-Agent", agent);
		return request;
	}
}
