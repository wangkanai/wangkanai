using Microsoft.AspNetCore.Http;

namespace Wangkanai.IdentityAdmin
{
    internal static class MockService
    {
        
        
        #region Internal

        private static IHttpContextAccessor HttpContextAccessor()
            => new HttpContextAccessor {HttpContext = CreateContext()};

        private static HttpContext CreateContext()
        {
            var context = new DefaultHttpContext();
            context.Request.Headers.Add("User-Agent", new[] {"MockClient"});
            return context;
        }

        #endregion
    }
}