// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

internal static class CommandLineApplicationExtensions
{
    internal static CommandLineApplication ProcessArgumentAssigned(this CommandLineApplication command, string arg, ref bool processed)
    {
        var currentCommand = command;
        foreach (var subcommand in command.Commands)
            if (string.Equals(subcommand.Name, arg, StringComparison.OrdinalIgnoreCase))
            {
                processed = true;
                command   = subcommand;
                break;
            }

        if (command != currentCommand)
            processed = true;
        return command;
    }

    
    internal static void ProcessArguments(this CommandLineApplication command, string arg, ref IEnumerator<CommandArgument>? arguments, ref bool processed)
    {
        if (arguments == null)
            arguments = new CommandArgumentEnumerator(command.Arguments.GetEnumerator());

        if (arguments.MoveNext())
        {
            processed = true;
            arguments.Current.Values.Add(arg);
        }
    }
    
    internal static void OptionMissingValue(this CommandLineApplication command, CommandOption option)
    {
        command.ShowHint();
        throw new CommandParsingException(command, $"Missing value for option '{option.LongName}'");
    }
    
}