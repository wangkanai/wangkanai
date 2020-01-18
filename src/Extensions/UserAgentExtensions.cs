// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class UserAgentExtensions
    {
        public static bool IsNullOrEmpty(this UserAgent agent)
            => agent == null
            || string.IsNullOrEmpty(agent.ToString());

        public static int Length(this UserAgent agent)
            => agent.ToString().Length;

        public static bool Contains(this UserAgent agent, string word)
            => agent.ToString().ToLower().ToLowerInvariant().Contains(word);

        public static bool StartsWith(this UserAgent agent, string word)
            => agent.ToString().StartsWith(word);
    }
}
