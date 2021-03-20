// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wangkanai.Detection.Extensions
{
    public static class VersionExtensions
    {
        private static readonly Regex VersionCleanupRegex = new Regex(@"\+|\-|\s|beta",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
        
        public static Version ToVersion(this string version)
        {
            if (string.IsNullOrEmpty(version))
                return new Version();


            version = VersionCleanupRegex.Replace(version, string.Empty);

            if (!version.Contains(".", StringComparison.Ordinal))
                version += ".0";

            return Version.TryParse(version, out var parsedVersion)
                       ? parsedVersion
                       : new Version(0, 0);
        }
    }
}