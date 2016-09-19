// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Detection
{
    public interface IDetectionService
    {
        HttpContext Context { get; }
        IUserAgent UserAgent { get; }
    }
}
