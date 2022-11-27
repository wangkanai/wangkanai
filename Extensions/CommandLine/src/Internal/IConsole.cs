// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine.Internal;

public interface IConsole
{
    event ConsoleCancelEventHandler CancelKeyPress;

    TextWriter   Out                { get; }
    TextWriter   Error              { get; }
    TextReader   In                 { get; }
    bool         IsInputRedirected  { get; }
    bool         IsOutputRedirected { get; }
    bool         IsErrorRedirected  { get; }
    ConsoleColor ForegroundColor    { get; set; }

    void ResetColor();
}