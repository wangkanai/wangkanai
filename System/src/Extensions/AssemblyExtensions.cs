// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class AssemblyExtensions
{
	[DebuggerStepThrough]
	public static string GetVersion(this Assembly assembly)
		=> assembly.GetName().Version?.ToString();

	[DebuggerStepThrough]
	public static string GetVersion(this Type type)
		=> type.Assembly.GetVersion();

	[DebuggerStepThrough]
	public static string GetInformationalVersion(this Assembly assembly)
		=> assembly.GetCustomAttributes(false)
		           .OfType<AssemblyInformationalVersionAttribute>()
		           .Single()
		           .InformationalVersion;

	[DebuggerStepThrough]
	public static string GetFileVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();

		var info    = FileVersionInfo.GetVersionInfo(assembly.Location);
		var version = info.FileBuildPart.ToString();
		return version;
	}
}