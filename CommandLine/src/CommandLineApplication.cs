// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;

namespace Wangkanai.Extensions.CommandLine;

public class CommandLineApplication
{
    private readonly bool _throwOnUnexpectedArg;
    private readonly bool _continueAfterUnexpectedArg;
    private readonly bool _treadUnmatchedOptionsAsArguments;

    public CommandLineApplication(bool throwOnUnexpectedArg = true, bool continueAfterUnexpectedArg = false, bool treadUnmatchedOptionsAsArguments = false)
    {
        _throwOnUnexpectedArg             = throwOnUnexpectedArg;
        _continueAfterUnexpectedArg       = continueAfterUnexpectedArg;
        _treadUnmatchedOptionsAsArguments = treadUnmatchedOptionsAsArguments;
    }

    public CommandLineApplication Parent { get; set; }

    public string Name                 { get; set; }
    public string FullName             { get; set; }
    public string Syntax               { get; set; }
    public string Description          { get; set; }
    public bool   ShowInHelpText       { get; set; } = true;
    public string ExtendedHelpText     { get; set; }
    public bool   IsShowingInformation { get; set; }

    public readonly List<string>          RemainingArgements;
    public readonly List<CommandArgument> Arguments;
}