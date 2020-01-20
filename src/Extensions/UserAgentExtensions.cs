// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class UserAgentExtensions
    {
        public static UserAgent UserAgentFromHeader(this HttpContext context)
            => new UserAgent(context.Request.Headers["User-Agent"].FirstOrDefault());

        public static bool IsNullOrEmpty(this UserAgent agent)
            => agent == null
               || string.IsNullOrEmpty(agent.ToLower());

        public static string ToLower(this UserAgent agent)
            => agent.ToString().ToLower();

        public static int Length(this UserAgent agent)
            => agent.ToString().Length;

        public static bool Contains(this UserAgent agent, string word)
            => agent.ToLower().Contains(word.ToLower());

        public static bool Contains(this UserAgent agent, string[] array)
            => array.Any(agent.Contains)
               && !agent.IsNullOrEmpty();

        public static bool Contains<T>(this UserAgent agent, T t)
            => agent.Contains(t.ToString().ToLower());

        public static bool StartsWith(this UserAgent agent, string word)
            => agent.ToLower().StartsWith(word.ToLower())
               && !agent.IsNullOrEmpty();

        public static bool StartsWith(this UserAgent agent, string[] array)
            => array.Any(agent.StartsWith);

        public static bool StartsWith(this UserAgent agent, string[] array, int minimum)
            => agent.Length() >= minimum
               && agent.StartsWith(array);
    }
}
