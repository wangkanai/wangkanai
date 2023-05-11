using System.Net.Http;

namespace Wangkanai.IdentityAdmin
{
    internal static class MockClient
    {
        public static HttpRequestMessage Request(string url = "/") 
            => new HttpRequestMessage(HttpMethod.Get, url);
    }
}