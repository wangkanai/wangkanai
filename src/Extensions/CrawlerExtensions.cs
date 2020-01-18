// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class CrawlerExtensions
    {
        public static Crawler Search<T>(
            this IEnumerable<T> list,
            UserAgent agent)
        {
            return Crawler.Unknown;
        }
    }
}
