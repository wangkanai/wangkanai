// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class AssemblyExtensions
{
	public static string GetVersion(this Assembly assembly)
	{
		return assembly.GetName().Version.ToString();
	}

	public static string GetVersion(this Type type)
	{
		return type.Assembly.GetVersion();
	}

	public static string GetInformationalVersion(this Assembly assembly)
	{
		return assembly.GetCustomAttributes(false)
		               .OfType<AssemblyInformationalVersionAttribute>()
		               .Single()
		               .InformationalVersion;
	}

	public static string GetFileVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();

		var info    = FileVersionInfo.GetVersionInfo(assembly.Location);
		var version = info.FileBuildPart.ToString();
		return version;
	}
}