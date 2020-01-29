using System.Net.Http;

namespace Microsoft.AspNetCore.Http
{
    internal static class HttpRequestMessageExtensions
    {
        public static void SetUserAgent(this HttpRequestMessage request, string agent)
            => request.Headers.Add("User-Agent", agent);
    }
}