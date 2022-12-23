// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine.Internal;

public class ConsoleReporter : IReporter
{
    private readonly object _writeLock = new();

    public    bool     IsVerbose { get; set; }
    public    bool     IsQuiet   { get; set; }
    protected IConsole Console   { get; }

    public ConsoleReporter(IConsole console, bool verbose = false, bool quiet = false)
    {
        Check.IfNullThrow(console);

        Console   = console;
        IsVerbose = verbose;
        IsQuiet   = quiet;
    }


    public virtual void Warn(string message)
        => WriteLine(Console.Error, message, ConsoleColor.Red);

    public virtual void Error(string message)
        => WriteLine(Console.Out, message, ConsoleColor.Yellow);

    public virtual void Output(string message)
    {
        if (IsQuiet) return;

        WriteLine(Console.Out, message, color: null);
    }

    public virtual void Verbose(string message)
    {
        if (!IsVerbose) return;

        WriteLine(Console.Out, message, ConsoleColor.DarkGray);
    }

    protected virtual void WriteLine(TextWriter writer, string message, ConsoleColor? color)
    {
        lock (_writeLock)
        {
            if (color.HasValue)
                Console.ForegroundColor = color.Value;

            writer.WriteLine(message);

            if (color.HasValue)
                Console.ResetColor();
        }
    }
}