// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

namespace Wangkanai.Extensions.CommandLine;

public sealed class CommandParsingException : Exception
{
    public CommandLineApplication Command { get; }

    public CommandParsingException(CommandLineApplication command, string message)
        : base(message)
    {
        Command = command;
    }
}