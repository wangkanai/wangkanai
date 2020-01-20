// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Responsive
{
    public class UserPreference
    {
        public UserPreference(string resolver, string cookie)
        {
            Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            Cookie = cookie ?? throw new ArgumentNullException(nameof(cookie));
        }

        public UserPreference(Device resolver, Device cookie) : this(resolver.ToString(), cookie.ToString())
        {
        }

        public string Resolver { get; }

        public string Cookie { get; }

        public string Preferred => Resolver != Cookie ? Cookie : Resolver;
    }
}
