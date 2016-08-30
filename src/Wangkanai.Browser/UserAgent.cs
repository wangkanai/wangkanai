// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Browser
{
    public class UserAgent
    {
        private readonly string agent;

        public UserAgent(string agent)
        {
            this.agent = agent;
        }

        public override string ToString()
        {
            return agent;
        }
    }
}