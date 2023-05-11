// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.System.Reflection;

public static class AssemblyVersion
{
    public static string Version
        => AssemblyInformationVersion<AssemblyInformationalVersionAttribute>().InformationalVersion;

    private static Assembly EntryAssembly
        => Assembly.GetEntryAssembly().ThrowIfNull();

    private static T AssemblyInformationVersion<T>()
        where T : Attribute
        => EntryAssembly.GetCustomAttribute<T>().ThrowIfNull();
}