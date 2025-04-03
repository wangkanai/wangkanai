// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Reflection;

/// <summary>
/// Provides utility methods and properties for retrieving version information of the assembly.
/// </summary>
public static class AssemblyVersion
{
	private static T AssemblyInformationVersion<T>() where T : Attribute
		=> EntryAssembly.GetCustomAttribute<T>().ThrowIfNull()!;

	private static readonly Assembly EntryAssembly = Assembly.GetEntryAssembly().ThrowIfNull()!;

	/// <summary>
	/// Represents the version of the assembly as retrieved from the assembly's informational version attribute.
	/// </summary>
	/// <remarks>
	/// The version is determined by extracting the informational version from the entry assembly's metadata,
	/// which is specified using the <see cref="AssemblyInformationalVersionAttribute"/> attribute.
	/// </remarks>
	public static readonly string Version = AssemblyInformationVersion<AssemblyInformationalVersionAttribute>().InformationalVersion;
}
