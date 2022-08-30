// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Extensions;

internal static class UserAgentExtensions
{
    public static bool IsNullOrEmpty(this UserAgent agent)
    {
        return string.IsNullOrEmpty(agent.ToString());
    }

    public static bool Contains(this UserAgent agent, string word)
    {
        return !agent.IsNullOrEmpty()
               && !word.IsNullOrEmpty()
               && agent.ToLower().Contains(word.ToLower());
    }

    public static bool Contains(this UserAgent agent, string[] array)
    {
        return !agent.IsNullOrEmpty()
               && array.Length > 0
               && array.AnyContains(agent);
    }

    public static bool Contains<T>(this UserAgent agent, T flags) where T : Enum
    {
        var strFlags = flags.ToString();
        return strFlags.Contains(',')
                   ? flags.GetFlags().Any(agent.Contains)
                   : agent.Contains(strFlags);
    }

    public static bool Contains(this UserAgent agent, IEnumerable<string> list)
    {
        return !agent.IsNullOrEmpty()
               && list.Any(agent.Contains);
    }

    public static UserAgent Replace(this UserAgent agent, string oldValue, string newValue)
    {
        return agent.IsNullOrEmpty() && oldValue.IsNullOrEmpty()
                   ? agent
                   : new UserAgent(agent.ToLower().Replace(oldValue, newValue));
    }

    public static int IndexOf(this UserAgent agent, string word)
    {
        return agent.ToLower()
                    .IndexOf(word.ToLower(), StringComparison.Ordinal);
    }

    public static int IndexOf(this UserAgent agent, Browser browser)
    {
        return agent.IndexOf(browser.ToString());
    }

    public static string Substring(this UserAgent agent, int start)
    {
        return agent.ToLower()[start..];
    }

    public static string Substring(this UserAgent agent, int start, int length)
    {
        return agent.ToLower()
                    .Substring(start, length);
    }

    public static string[] Split(this UserAgent agent, char separator)
    {
        return agent.ToLower()
                    .Split(separator);
    }

    public static bool StartsWith(this UserAgent agent, string word)
    {
        return !word.IsNullOrEmpty()
               && !agent.IsNullOrEmpty()
               && agent.ToLower()
                       .StartsWith(word.ToLower());
    }

    public static bool StartsWith(this UserAgent agent, string[] array)
    {
        return array.AnyStartsWith(agent);
    }

    public static bool StartsWith(this UserAgent agent, string[] array, int minimum)
    {
        return agent.Length() >= minimum
               && agent.StartsWith(array);
    }

    private static bool AnyStartsWith(this string[] array, UserAgent agent)
    {
        return array.Any(agent.StartsWith);
    }

    private static bool AnyContains(this string[] array, UserAgent agent)
    {
        return array.Any(agent.Contains);
    }
}