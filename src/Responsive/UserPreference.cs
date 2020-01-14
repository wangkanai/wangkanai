// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Responsive
{
    public class UserPreference
    {
        public string Resolver { get; set; }

        public string Cookie { get; set; }

        public string Preferred => (Resolver != Cookie) ? Cookie : Resolver;

        public UserPreference(string resolver, string cookie)
        {
            if (resolver is null)
                throw new System.ArgumentNullException(nameof(resolver));
            if (cookie is null)
                throw new System.ArgumentNullException(nameof(cookie));

            Resolver = resolver;
            Cookie = cookie;
        }

        public UserPreference(Device resolver, Device cookie)
            : this(resolver.ToString(), cookie.ToString()) { }
    }
}
