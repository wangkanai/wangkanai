using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.IdentityAdmin.Configuration;

namespace Wangkanai.IdentityAdmin
{
    internal static class MockServer
    {
        public static TestServer Server<TUser, TRole>()
            => Server(WebHostBuilder<TUser, TRole>());
        
        public static TestServer Server<TUser, TRole>(Action<IdentityAdminOptions> options)
            => Server(WebHostBuilder<TUser, TRole>(options));

        private static TestServer Server(IWebHostBuilder builder)
            => new TestServer(builder);

        public static IWebHostBuilder WebHostBuilder<TUser, TRole>()
            => WebHostBuilder<TUser, TRole>(options => { });
        
        public static IWebHostBuilder WebHostBuilder<TUser, TRole>(Action<IdentityAdminOptions> options)
            => new WebHostBuilder()
               .ConfigureServices(services => services.AddIdentityAdmin<TUser, TRole>(options))
               .Configure(app =>
               {
                   app.UseIdentityAdmin();
                   app.Run(ContextHandler());
               });

        private static RequestDelegate ContextHandler() 
            => context => context.Response.WriteAsync("IdentityAdmin");
    }
}