// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Extensions;

internal static class UserAgentExtensions
{
   public static bool IsNullOrEmpty(this UserAgent agent)
      => string.IsNullOrEmpty(agent.ToString());

   public static bool Contains(this UserAgent agent, string word)
      => !agent.IsNullOrEmpty()
         && !word.IsNullOrEmpty()
         && agent.ToString().Contains(word, StringComparison.OrdinalIgnoreCase);

   public static bool Contains(this UserAgent agent, string[] array)
      => !agent.IsNullOrEmpty()
         && array.Length > 0
         && array.AnyContains(agent);

   public static bool Contains<T>(this UserAgent agent, T flags) where T : Enum => flags.GetFlags().Any(f => agent.Contains(f.ToString()));

   public static bool Contains(this UserAgent agent, IEnumerable<string> list)
      => !agent.IsNullOrEmpty()
         && list.Any(agent.Contains);

   public static UserAgent Replace(this UserAgent agent, string oldValue, string newValue)
      => agent.IsNullOrEmpty() && oldValue.IsNullOrEmpty()
         ? agent
         : new(agent.ToLower().Replace(oldValue, newValue));

   public static int IndexOf(this UserAgent agent, string word)
      => agent.ToLower()
              .IndexOf(word.ToLower(), StringComparison.Ordinal);

   public static int IndexOf(this UserAgent agent, Browser browser)
      => agent.IndexOf(browser.ToString());

   public static string Substring(this UserAgent agent, int start)
      => agent.ToLower()[start..];

   public static string Substring(this UserAgent agent, int start, int length)
      => agent.ToLower()
              .Substring(start, length);

   public static string[] Split(this UserAgent agent, char separator)
      => agent.ToLower()
              .Split(separator);

   public static bool StartsWith(this UserAgent agent, string word)
      => !word.IsNullOrEmpty()
         && !agent.IsNullOrEmpty()
         && agent.ToLower().StartsWith(word, StringComparison.CurrentCultureIgnoreCase);

   public static bool StartsWith(this UserAgent agent, string[] array)
      => array.AnyStartsWith(agent);

   public static bool StartsWith(this UserAgent agent, string[] array, int minimum)
      => agent.Length() >= minimum
         && agent.StartsWith(array);

   private static bool AnyStartsWith(this string[] array, UserAgent agent)
      => array.Any(agent.StartsWith);

   private static bool AnyContains(this string[] array, UserAgent agent)
      => array.Any(agent.Contains);
}