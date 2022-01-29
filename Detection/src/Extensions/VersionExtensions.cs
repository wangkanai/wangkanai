// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Text.RegularExpressions;

namespace Wangkanai.Detection.Extensions;

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