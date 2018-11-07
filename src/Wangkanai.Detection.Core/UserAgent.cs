// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class UserAgent : IUserAgent
    {
        private readonly string useragent;

        public UserAgent() { }
        public UserAgent(string useragent)
        {
            if (useragent != null)
                this.useragent = useragent;
            else
                this.useragent = string.Empty;
        }

        public override string ToString()
        {
            return useragent;
        }
    }
}