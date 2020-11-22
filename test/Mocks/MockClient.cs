// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Net.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Mocks
{
    internal static class MockClient
    {
        public static HttpRequestMessage CreateRequest(Device agent, string url = "/")
            => CreateRequest(agent.ToString(), url);

        public static HttpRequestMessage CreateRequest(string agent, string url = "/")
        {
            var context = MockService.HttpContextService(agent);
            context.Request.Headers.Add("User-Agent", agent);
            context.Request.Path = url;
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            return request;
        }
    }
}