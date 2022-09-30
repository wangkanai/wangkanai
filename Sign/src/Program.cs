// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;
using System.Windows.Input;

using Wangkanai.Reflection;
using Wangkanai.Sign.Internal;

namespace Wangkanai.Sign;

public class Program
{
    private readonly IConsole _console;
    private readonly string   _workingDirectory;

    public static int Main(string[] args)
    {
        DebugHelper.HandleDebugSwitch(ref args);

        int rc;
        new Program(PhysicalConsole.Singleton, Directory.GetCurrentDirectory()).TryRun(args, out rc);
        return rc;
    }

    public Program(IConsole console, string workingDirectory)
    {
        _console          = console;
        _workingDirectory = workingDirectory;
    }

    public bool TryRun(string[] args, out int returnCode)
    {
        // awaiting to be implemented
        returnCode = 1;
        return true;
    }
}