﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

internal static class ExecuteExtensions
{
	internal static bool FindOptionHelpVersion(this CommandLineApplication command, CommandOption option)
	{
		if (command.OptionHelp == option)
		{
			command.ShowHelp();
			return true;
		}

		if (command.OptionVersion == option)
		{
			command.ShowVersion();
			return true;
		}

		return false;
	}

	internal static bool ProcessArguments(this CommandLineApplication command, string arg, ref IEnumerator<CommandArgument>? arguments, ref bool processed, ref bool argumentsAssigned)
	{
		arguments ??= new CommandArgumentEnumerator(command.Arguments.GetEnumerator());

		if (!arguments.MoveNext())
			return false;

		processed = true;
		arguments.Current.Values.Add(arg);
		argumentsAssigned = true;

		return true;
	}

	internal static bool ProcessArguments(this CommandLineApplication command, string arg, ref IEnumerator<CommandArgument>? arguments, ref bool processed)
	{
		arguments ??= new CommandArgumentEnumerator(command.Arguments.GetEnumerator());

		if (!arguments.MoveNext())
			return false;

		processed = true;
		arguments.Current.Values.Add(arg);

		return true;
	}

	internal static void ProcessOptionThrowException(this CommandLineApplication command, string arg, ref bool processed, ref CommandOption? option)
	{
		processed = true;
		if (option != null && !option.TryParse(arg))
		{
			command.ShowHint();
			throw new CommandParsingException(command, $"Unexpected value '{arg}' for option '{option.LongName}'");
		}

		option = null;
	}

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

		// If we detect a subcommand
		if (command != currentCommand)
			processed = true;
		return command;
	}


	internal static void OptionMissingValue(this CommandLineApplication command, CommandOption option)
	{
		command.ShowHint();
		throw new CommandParsingException(command, $"Missing value for option '{option.LongName}'");
	}
}