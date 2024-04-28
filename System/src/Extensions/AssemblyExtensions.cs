// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class AssemblyExtensions
{
	[DebuggerStepThrough]
	public static Version? GetVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var name = assembly.GetName().ThrowIfNull();
		return name.Version;
	}

	[DebuggerStepThrough]
	public static string GetVersionString(this Assembly assembly)
	{
		var version = assembly.GetVersion();
		version.ThrowIfNull();
		return version.ToString();
	}

	[DebuggerStepThrough]
	public static Version? GetVersion(this Type type)
	{
		type.ThrowIfNull();
		var assembly = type.Assembly.ThrowIfNull();
		return assembly.GetVersion();
	}

	[DebuggerStepThrough]
	public static string GetVersionString(this Type type)
	{
		var version = type.GetVersion();
		version.ThrowIfNull();
		return version.ToString();
	}

	[DebuggerStepThrough]
	public static string GetInformationalVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var info    = assembly.GetCustomAttributes(false).OfType<AssemblyInformationalVersionAttribute>().Single();
		var version = info.InformationalVersion;
		return version;
	}

	[DebuggerStepThrough]
	public static string GetFileVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var info    = FileVersionInfo.GetVersionInfo(assembly.Location);
		var version = info.FileVersion.ThrowIfNull();
		return version;
	}
}
