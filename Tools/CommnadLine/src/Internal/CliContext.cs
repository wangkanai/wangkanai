// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine.Internal;

/// <summary>
/// This API supports infrastructure and is not intended to be used
/// directly from your code. This API may change or be removed in future releases.
/// </summary>
public static class CliContext
{
    /// <summary>
    /// dotnet -d/--diagnostics subcommand
    /// </summary>
    /// <returns></returns>
    public static bool IsGlobalVerbose()
    {
        bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_CLI_CONTEXT_VERBOSE"), out bool globalVerbose);
        return globalVerbose;
    }
}