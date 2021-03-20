using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection.Services
{
    /// <summary>
    /// Provides the APIs for HttpContext.
    /// </summary>
    public interface IHttpContextService
    {
        /// <summary>
        /// Get the <see cref="HttpContext"/> of the application service.
        /// </summary>
        HttpContext Context { get; }
        
        HttpRequest Request { get; }
    }
}