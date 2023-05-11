// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.System.Extensions.CommandLine.Internal;

public class NullReporter : IReporter
{
    private NullReporter() { }

    public static IReporter Singleton { get; } = new NullReporter();

    public void Warn(string    message) { }
    public void Error(string   message) { }
    public void Output(string  message) { }
    public void Verbose(string message) { }
}