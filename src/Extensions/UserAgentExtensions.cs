// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Extensions
{
    internal static class UserAgentExtensions
    {
        public static bool IsNullOrEmpty(this UserAgent agent)
            => agent is null
               || string.IsNullOrEmpty(agent.ToLower());
        
        public static int Length(this UserAgent agent)
            => agent.ToString().Length;

        public static bool Contains(this UserAgent agent, string word)
            => !word.IsNullOrEmpty()
               && !agent.IsNullOrEmpty()
               && agent.ToLower().Contains(word.ToLower());

        public static bool Contains(this UserAgent agent, string[] array)
            => !agent.IsNullOrEmpty()
               && array.Length > 0
               && array.AnyContains(agent);
        

        public static bool Contains<T>(this UserAgent agent, T flags) where T : Enum
        {
            var strFlags = flags.ToString();
            return strFlags.Contains(',')
                       ? flags.GetFlags().Any(agent.Contains)
                       : agent.Contains(strFlags);
        }

        public static bool Contains(this UserAgent agent, IEnumerable<string> list)
            => !agent.IsNullOrEmpty()
               && list is { }
               && list.Any(agent.Contains);
        
        public static UserAgent Replace(this UserAgent agent, string oldValue, string newValue)
        {
            if (agent.IsNullOrEmpty() && oldValue.IsNullOrEmpty())
                return agent;
            return new UserAgent(agent.ToLower().Replace(oldValue,newValue));
        }

        public static int IndexOf(this UserAgent agent, string word)
            => agent.ToLower()
                    .IndexOf(word.ToLower(), StringComparison.Ordinal);

        public static int IndexOf(this UserAgent agent, Browser browser)
            => agent.IndexOf(browser.ToString());
        
        public static string Substring(this UserAgent agent, int startindex)
            => agent.ToLower().Substring(startindex);

        public static string Substring(this UserAgent agent, int startindex, int length)
            => agent.ToLower().Substring(startindex, length);
        
        public static bool StartsWith(this UserAgent agent, string word)
            => !word.IsNullOrEmpty()
               && !agent.IsNullOrEmpty()
               && agent.ToLower().StartsWith(word.ToLower());

        public static bool StartsWith(this UserAgent agent, string[] array)
            => array.AnyStartsWith(agent);

        public static bool StartsWith(this UserAgent agent, string[] array, int minimum)
            => agent.Length() >= minimum
               && agent.StartsWith(array);

        private static bool AnyStartsWith(this string[] array, UserAgent agent)
        {
            foreach (var str in array)
            {
                if (agent.StartsWith(str)) return true;
            }

            return false;
        }
        
        private static bool AnyContains(this string[] array, UserAgent agent)
        {
            foreach (var str in array)
            {
                if (agent.Contains(str)) return true;
            }

            return false;
        }
    }
}