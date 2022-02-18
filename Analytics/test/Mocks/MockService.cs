using Microsoft.AspNetCore.Http;

namespace Wangkanai.Analytics.Tests.Mocks;

public class MockService
{
    private static IHttpContextAccessor HttpContextAccessor()
        => new HttpContextAccessor {HttpContext = new DefaultHttpContext()};
}