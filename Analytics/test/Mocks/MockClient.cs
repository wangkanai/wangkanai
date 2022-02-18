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