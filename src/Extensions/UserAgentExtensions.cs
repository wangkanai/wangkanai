// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class UserAgentExtensions
    {
        public static bool IsNullOrEmpty(this UserAgent agent)
            => agent is null || string.IsNullOrEmpty(agent.ToString());
    }
}
