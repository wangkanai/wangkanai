// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class CrawlerExtensions
    {
        public static bool Search(this IEnumerable<string> list, UserAgent agent)
            => list != null && list.Any(agent.Contains);

        public static IEnumerable<Crawler> All()
            => Enum.GetValues(typeof(Crawler)).Cast<Crawler>();
    }
}
