// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public interface IResponsiveService
    {
        HttpContext Context { get; }
        IUserAgent UserAgent { get; }
    }
}