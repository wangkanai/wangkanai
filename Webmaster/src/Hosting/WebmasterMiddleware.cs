using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace Wangkanai.Webmaster;

//[Experimental()]
//[Deprecated()]
//[Obsolete()]
public class WebmasterMiddleware
{
    private readonly RequestDelegate _next;

    public WebmasterMiddleware(RequestDelegate next)
    {
        _next = Check.NotNull(next);
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Check.NotNull(context);

        await _next(context).ConfigureAwait(false);
    }
}