// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
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
            => !word.IsNullOrEmpty()
               && !agent.IsNullOrEmpty()
               && agent.ToLower().Contains(word.ToLower());

        public static bool Contains(this UserAgent agent, string[] array)
            => !agent.IsNullOrEmpty()
               && array.Length > 0
               && array.Any(agent.Contains);

        public static bool Contains<T>(this UserAgent agent, T flags) where T : Enum
            => flags.ToString().Contains(',')
                ? flags.GetFlags().Any(agent.Contains)
                : agent.Contains(flags.ToString());

        public static bool Contains(this UserAgent agent, IEnumerable<string> list)
            => list != null
               && agent.IsNullOrEmpty()
               && list.Any(agent.Contains);

        public static bool StartsWith(this UserAgent agent, string word)
            => !word.IsNullOrEmpty()
               && !agent.IsNullOrEmpty()
               && agent.ToLower().StartsWith(word.ToLower());

        public static bool StartsWith(this UserAgent agent, string[] array)
            => array.Any(agent.StartsWith);

        public static bool StartsWith(this UserAgent agent, string[] array, int minimum)
            => agent.Length() >= minimum
               && agent.StartsWith(array);
    }
}
