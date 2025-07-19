// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

/// <summary>
/// Extensions for working with assemblies.
/// </summary>
[DebuggerStepThrough]
public static class AssemblyExtensions
{
	/// <summary>
	/// Gets the version of the assembly.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	/// <returns>The version of the assembly.</returns>
	public static Version? GetVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var name = assembly.GetName().ThrowIfNull();
		return name.Version;
	}

	/// <summary>
	/// Gets the version of the assembly.
	/// </summary>
	/// <param name="type">The assembly type.</param>
	/// <returns>The version of the assembly.</returns>
	public static Version? GetVersion(this Type type)
	{
		type.ThrowIfNull();
		var assembly = type.Assembly.ThrowIfNull();
		return assembly.GetVersion();
	}

	/// <summary>
	/// Gets the version of the assembly as a string.
	/// </summary>
	/// <param name="type">The assembly type.</param>
	/// <returns>The version of the assembly as a string.</returns>
	public static string GetVersionString(this Type type)
	{
		var version = type.GetVersion();
		version.ThrowIfNull();
		return version.ToString();
	}

	/// <summary>
	/// Gets the version of the assembly as a string.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	/// <returns>The version of the assembly as a string.</returns>
	public static string GetVersionString(this Assembly assembly)
	{
		var version = assembly.GetVersion();
		version.ThrowIfNull();
		return version.ToString();
	}

	/// <summary>
	/// Gets the informational version of the assembly.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	/// <returns>The informational version of the assembly.</returns>
	public static string GetInformationalVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var info = assembly.GetCustomAttributes(false).OfType<AssemblyInformationalVersionAttribute>().Single();
		var version = info.InformationalVersion;
		return version;
	}

	/// <summary>
	/// Gets the file version of the assembly.
	/// </summary>
	/// <param name="assembly">The assembly.</param>
	/// <returns>The file version of the assembly as a string.</returns>
	public static string GetFileVersion(this Assembly assembly)
	{
		assembly.ThrowIfNull();
		var info = FileVersionInfo.GetVersionInfo(assembly.Location);
		var version = info.FileVersion.ThrowIfNull();
		return version;
	}
}
