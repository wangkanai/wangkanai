// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;
using System.Globalization;

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

    public string Name                   { get; set; }
    public string FullName               { get; set; }
    public string Syntax                 { get; set; }
    public string Description            { get; set; }
    public bool   ShowInHelpText         { get; set; } = true;
    public string ExtendedHelpText       { get; set; }
    public bool   IsShowingInformation   { get; set; }
    public bool   AllowArgumentSeparator { get; set; }

    public readonly List<CommandArgument> Arguments;
    public readonly List<CommandOption>   Options;
    public readonly List<string>          RemainingArgements;

    public CommandOption OptionHelp    { get; private set; }
    public CommandOption OptionVersion { get; private set; }

    public Func<int>    Invoke             { get; set; }
    public Func<string> LongVersionGetter  { get; set; }
    public Func<string> ShortVersionGetter { get; set; }

    public readonly List<CommandLineApplication> Commands;

    public TextWriter Out   { get; set; }
    public TextWriter Error { get; set; }

    public IEnumerable<CommandOption> GetOptions()
    {
        var expr     = Options.AsEnumerable();
        var rootNode = this;
        while (rootNode.Parent != null)
        {
            rootNode = rootNode.Parent;
            expr     = expr.Concat(rootNode.Options);
        }

        return expr;
    }

    public CommandLineApplication Command(string name, Action<CommandLineApplication> configuration, bool throwOnUnexpectedArg = true)
    {
        var command = new CommandLineApplication(throwOnUnexpectedArg) { Name = name };
        Commands.Add(command);
        configuration(command);
        return command;
    }

    public CommandOption Option(string templete, string description, CommandOptionType optionType)
        => Option(templete, description, optionType, _ => { }, inherited: false);

    public CommandOption Option(string templete, string description, CommandOptionType optionType, bool inherited)
        => Option(templete, description, optionType, _ => { }, inherited);

    public CommandOption Option(string templete, string description, CommandOptionType optionType, Action<CommandOption> configuration)
        => Option(templete, description, optionType, configuration, inherited: false);

    public CommandOption Option(string templete, string description, CommandOptionType optionType, Action<CommandOption> configuration, bool inherited)
    {
        var option = new CommandOption(templete, optionType)
        {
            Description = description,
            Inherited   = inherited
        };
        Options.Add(option);
        configuration(option);
        return option;
    }

    public CommandArgument Argument(string name, string description, bool multipleValues = false)
    {
        return Argument(name, description, _ => { }, multipleValues);
    }

    public CommandArgument Argument(string name, string description, Action<CommandArgument> configuration, bool multipleValues = false)
    {
        var lastArg = Arguments.LastOrDefault();
        if (lastArg != null && lastArg.MultipleValues)
        {
            var message = string.Format(
                CultureInfo.CurrentCulture,
                "The last argument '{0}' after multiple values. No more argument can be added.",
                lastArg.Name);
            throw new InvalidOperationException(message);
        }

        var argument = new CommandArgument { Name = name, Description = description, MultipleValues = multipleValues };
        Arguments.Add(argument);
        configuration(argument);
        return argument;
    }
}