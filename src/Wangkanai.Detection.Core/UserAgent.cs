// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class UserAgent : IUserAgent
    {
        private readonly string useragent;

        public UserAgent() { }
        public UserAgent(string useragent)
        {
            if (useragent != null)
                this.useragent = useragent.ToLowerInvariant();
        }

        public override string ToString()
        {
            return useragent;
        }
    }
}