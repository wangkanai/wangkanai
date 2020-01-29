using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class PreferenceSessionTests
    {
        [Fact]
        public async Task ReadingEmptySessionDoesNotCreateCookie()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddDetection();
                })
                .Configure(app =>
                {
                    app.UseDetection();
                    app.Run(context =>
                    {
                        context.Session.SetString("Key","Value");
                        return Task.FromResult(0);
                    });
                });

            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();
                var response = await client.GetAsync("/");
                response.EnsureSuccessStatusCode();
                Assert.True(response.Headers.TryGetValues("Set-Cookie", out var values));
                Assert.Single(values);
            }
        }
    }
}