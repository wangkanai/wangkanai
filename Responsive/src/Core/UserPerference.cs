// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class UserPerference
    {
        public string Resolver { get; set; }

        public string Cookie { get; set; }

        public string Preferred
        {
            get
            {
                return (Resolver != Cookie) ? Cookie : Resolver;
            }
        }

        public UserPerference() { }
        public UserPerference(string resolver, string cookie)
        {
            Resolver = resolver;
            Cookie = cookie;
        }

        public UserPerference(DeviceType resolver, DeviceType cookie)
            : this(resolver.ToString(), cookie.ToString())
        {
        }
    }
}
