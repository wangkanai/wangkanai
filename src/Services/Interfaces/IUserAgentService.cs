// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    /// <summary>
    /// Provides the APIs for query client access device.
    /// </summary>
    public interface IUserAgentService
    {
        /// <summary>
        /// Get the <see cref="HttpContext"/> of the application service.
        /// </summary>
        HttpContext? Context { get; }

        /// <summary>
        /// Get the <see cref="UserAgent"/> of the request client.
        /// </summary>
        UserAgent UserAgent { get; }
    }
}
