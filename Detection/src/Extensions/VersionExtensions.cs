// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Text.RegularExpressions;

namespace Wangkanai.Detection.Extensions;

public static class VersionExtensions
{
	private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase;
	private static readonly Regex CleanupRegex = new(@"\+|\-|\s|beta", Options, Constants.RegexTimeout);

	public static Version ToVersion(this string version)
	{
		if (string.IsNullOrEmpty(version))
			return new Version();

		version = CleanupRegex.Replace(version, string.Empty);

		if (!version.Contains('.', StringComparison.Ordinal))
			version += ".0";

		if (version.Contains(',', StringComparison.Ordinal))
			version = version.Replace(",", ".");

		return Version.TryParse(version, out var parsedVersion)
				   ? parsedVersion
				   : new Version(0, 0);
	}
}
