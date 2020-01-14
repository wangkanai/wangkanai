// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace Wangkanai.Detection.Extensions
{
    public static class DetectionVersionExtensions
    {
        [DebuggerStepThrough]
        public static Version ToVersion(this string version)
        {
            version = version.ToLower()
                .RemoveBeta()
                .RemoveWhitespace()
                .RemovePlus()
                .RemoveMinus();

            return Version.TryParse(version, out var parsedVersion) ?
                parsedVersion :
                new Version(0, 0);
        }

        /// <summary>
        /// if request is going via google proxy then version will be appended with Firefox/11.0 (via ggpht.com GoogleImageProxy)
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private static string RemoveWhitespace(this string version)
            => version.Contains(" ") ? version.Replace(" ", "") : version;

        private static string RemovePlus(this string version)
            => version.Contains("+") ? version.Replace("+", "") : version;

        private static string RemoveMinus(this string version)
            => version.Contains("-") ? version.Replace("-", "") : version;

        private static string RemoveBeta(this string version)
            => version.Contains("beta") ? version.Replace("beta", "") : version;
    }
}
