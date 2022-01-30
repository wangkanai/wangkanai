using Microsoft.AspNetCore.Builder;

using Wangkanai.Webmaster;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension method for adding the <see cref="WebmasterMiddleware"/> to the application.
/// </summary>
public static class WebmasterApplicationBuilderExtensions
{
    public static IApplicationBuilder UseWebmaster(this IApplicationBuilder app)
    {
        if (app is null)
            throw new ArgumentNullException(nameof(app));

        app.Validate();

        return app.UseMiddleware<WebmasterMiddleware>();
    }

    private static void Validate(this IApplicationBuilder app)
    {
        // What should I validate?
    }
}