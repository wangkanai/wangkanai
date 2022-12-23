// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public static class AssemblyExtensions
{
    public static string GetInformationalVersion(this Assembly assembly)
        => assembly.GetCustomAttributes(false)
                   .OfType<AssemblyInformationalVersionAttribute>()
                   .Single()
                   .InformationalVersion;

    public static string GetFileVersion(this Assembly assembly)
    {
        assembly.IfNullThrow();
        
        var info    = FileVersionInfo.GetVersionInfo(assembly.Location);
        var version = info.FileBuildPart.ToString();
        return version;
    }
}