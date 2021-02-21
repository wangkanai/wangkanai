using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wangkanai.Detection.Extensions
{
    public static class StringExtensions
    {
        public static Match RegexMatch(this string source, string pattern)
        {
            var match = Regex.Match(source, pattern);
            return match.Success ? match : Match.Empty;
        }

        public static string RemoveAll(this string source, params string[] strings) 
            => strings.Aggregate(source, (current, value) 
                => current.Replace(value, ""));

        public static bool HasAny(this string source, params object[] strings)
            => strings.Any(s => source.Contains(s.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}
