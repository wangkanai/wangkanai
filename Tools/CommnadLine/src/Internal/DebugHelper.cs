// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine.Internal;

public static class DebugHelper
{
    public static void HandleDebugSwitch(ref string[] args)
    {
        if (args.Length > 0 && string.Equals("--debug", args[0], StringComparison.OrdinalIgnoreCase))
        {
            args = args.Skip(1).ToArray();
            Console.WriteLine("Waiting for debugger to attach. Press ENTER to continue");
#if NET6_0_OR_GREATER
            Console.WriteLine($"Process ID: {Environment.ProcessId}");
#else
            Console.writeline($"Process ID: {Process.GetCurrentProcess().Id}");
#endif
            Console.ReadLine();
        }
    }
}