﻿// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;
using System.Text;

namespace Wangkanai.Extensions.CommandLine;

public sealed class CommandLineApplication
{
	private readonly bool _continueAfterUnexpectedArg;
	private readonly bool _treatUnmatchedOptionsAsArguments;
	private readonly bool _throwOnUnexpectedArg;

	public CommandLineApplication(bool throwOnUnexpectedArg = true, bool continueAfterUnexpectedArg = false, bool treatUnmatchedOptionsAsArguments = false)
	{
		_throwOnUnexpectedArg             = throwOnUnexpectedArg;
		_continueAfterUnexpectedArg       = continueAfterUnexpectedArg;
		_treatUnmatchedOptionsAsArguments = treatUnmatchedOptionsAsArguments;

		Options            = new List<CommandOption>();
		Arguments          = new List<CommandArgument>();
		Commands           = new List<CommandLineApplication>();
		RemainingArguments = new List<string>();
		Invoke             = () => 0;
	}

	internal readonly List<CommandOption>          Options;
	internal readonly List<CommandArgument>        Arguments;
	internal readonly List<CommandLineApplication> Commands;
	internal readonly List<string>                 RemainingArguments;

	public CommandLineApplication? Parent        { get; private set; }
	public CommandOption?          OptionHelp    { get; private set; }
	public CommandOption?          OptionVersion { get; private set; }

	public Func<int>     Invoke             { get; set; }
	public Func<string>? LongVersionGetter  { get; set; }
	public Func<string>? ShortVersionGetter { get; set; }

	public string? Name                   { get; set; }
	public string? FullName               { get; set; }
	public string? Syntax                 { get; set; }
	public string? Description            { get; set; }
	public string? ExtendedHelpText       { get; set; }
	public bool    ShowInHelpText         { get; set; }
	public bool    IsShowingInformation   { get; set; } // Is showing help or version?
	public bool    AllowArgumentSeparator { get; set; }

	public TextWriter Out   { get; set; } = Console.Out;
	public TextWriter Error { get; set; } = Console.Error;

	public CommandLineApplication Command(string name, Action<CommandLineApplication> configuration, bool throwOnUnexpectedArg = true)
	{
		var command = new CommandLineApplication(throwOnUnexpectedArg)
		{
			Name   = name,
			Parent = this
		};
		Commands.Add(command);
		configuration(command);
		return command;
	}

	public CommandOption Option(string template, string description, CommandOptionType optionType)
		=> Option(template, description, optionType, inherited: false);

	public CommandOption Option(string template, string description, CommandOptionType optionType, bool inherited)
		=> Option(template, description, optionType, _ => { }, inherited);

	public CommandOption Option(string template, string description, CommandOptionType optionType, Action<CommandOption> configuration)
		=> Option(template, description, optionType, configuration, inherited: false);

	public CommandOption Option(string template, string description, CommandOptionType optionType, Action<CommandOption> configuration, bool inherited)
	{
		var option = new CommandOption(template, optionType)
		{
			Description = description,
			Inherited   = inherited
		};
		Options.Add(option);
		configuration(option);
		return option;
	}

	public CommandArgument Argument(string name, string description, bool multipleValues = false)
		=> Argument(name, description, _ => { }, multipleValues);

	public CommandArgument Argument(string name, string description, Action<CommandArgument> configuration, bool multipleValues = false)
	{
		var lastArg = Arguments.LastOrDefault();
		if (lastArg != null && lastArg.MultipleValues)
		{
			var message = string.Format(
				CultureInfo.CurrentCulture,
				"The last argument '{0}' accepts multiple values. No more argument can be added.",
				lastArg.Name);
			throw new InvalidOperationException(message);
		}

		var argument = new CommandArgument
		{
			Name           = name,
			Description    = description,
			MultipleValues = multipleValues
		};
		Arguments.Add(argument);
		configuration(argument);
		return argument;
	}

	public void OnExecute(Func<int> invoke)
		=> Invoke = invoke;

	public void OnExecute(Func<Task<int>> invoke)
		=> Invoke = () => invoke().Result;

	private static readonly char[] separator = { ':', '=' };

	public int Execute(params string[] args)
	{
		CommandLineApplication        command   = this;
		CommandOption?                option    = null;
		IEnumerator<CommandArgument>? arguments = null;
		var                           assigned  = false;

		command.ThrowIfNull();

		for (var index = 0; index < args.Length; index++)
		{
			var arg       = args[index];
			var processed = false;
			if (!processed && option == null)
			{
				string[]? longOption  = null;
				string[]? shortOption = null;

				if (arg.StartsWith("--", StringComparison.Ordinal))
					longOption = arg.Substring(2).Split(separator, 2);
				else if (arg.StartsWith("-", StringComparison.Ordinal))
					shortOption = arg.Substring(1).Split(separator, 2);

				if (longOption != null)
				{
					processed = true;
					var longOptionName = longOption[0];
					option = command.GetOptions().SingleOrDefault(opt => string.Equals(opt.LongName, longOptionName, StringComparison.Ordinal));

					if (option == null && _treatUnmatchedOptionsAsArguments && command.ProcessArguments(arg, ref arguments, ref processed, ref assigned))
						continue;

					if (option == null)
					{
						var ignoreContinueAfterUnexpectedArg = false;
						if (longOptionName.IsNullOrEmpty() && !command._throwOnUnexpectedArg && AllowArgumentSeparator)
						{
							// Skip over the '--' argument separator then consume all remaining arguments. All
							// remaining arguments are unconditionally stored for further use.
							index++;
							ignoreContinueAfterUnexpectedArg = true;
						}

						if (HandleUnexpectedArg(command, args, index, argTypeName: "option", ignoreContinueAfterUnexpectedArg))
							continue;

						break;
					}

					// If we find a help/version option, show information and stop parsing
					if (command.OptionHelp == option)
						return command.ShowHelp();

					if (command.OptionVersion == option)
						return command.ShowVersion();

					if (longOption.Length == 2)
					{
						command.ShowHint(option, longOption);
						option = null;
					}
					else if (option.OptionType == CommandOptionType.NoValue)
					{
						// No value is needed for this option
						option.TryParse(null!);
						option = null;
					}
				}

				if (shortOption != null)
				{
					processed = true;
					option    = command.GetOptions().SingleOrDefault(opt => string.Equals(opt.ShortName, shortOption[0], StringComparison.Ordinal));

					if (option == null && _treatUnmatchedOptionsAsArguments && command.ProcessArguments(arg, ref arguments, ref processed, ref assigned))
						continue;

					// If not a short option, try symbol option
					if (option == null)
						option = command.GetOptions().SingleOrDefault(opt => string.Equals(opt.SymbolName, shortOption[0], StringComparison.Ordinal));

					if (option == null)
					{
						if (HandleUnexpectedArg(command, args, index, argTypeName: "option"))
							continue;

						break;
					}

					// If we find a help/version option, show information and stop parsing
					if (command.OptionHelp == option)
						return command.ShowHelp();

					if (command.OptionVersion == option)
						return command.ShowVersion();

					if (shortOption.Length == 2)
					{
						command.ShowHint(option, shortOption);
						option = null;
					}
					else if (option.OptionType == CommandOptionType.NoValue)
					{
						// No value is needed for this option
						option.TryParse(null!);
						option = null;
					}
				}
			}

			if (!processed && option != null)
				command.ProcessOptionThrowException(arg, ref processed, ref option);

			if (!processed && !assigned)
				command = command.ProcessArgumentAssigned(arg, ref processed);

			if (!processed)
				command.ProcessArguments(arg, ref arguments, ref processed);

			if (!processed)
			{
				if (HandleUnexpectedArg(command, args, index, argTypeName: "command or argument"))
					continue;

				break;
			}
		}

		if (option != null)
			command.OptionMissingValue(option);

		return command.Invoke();
	}

	// Helper method that adds a help option
	public CommandOption HelpOption(string template)
	{
		// Help option is special because we stop parsing once we see it
		// So we store it separately for further use
		OptionHelp = Option(template, "Show help information", CommandOptionType.NoValue);

		return OptionHelp;
	}

	public CommandOption VersionOption(string template, string shortFormVersion, string? longFormVersion = null)
		=> longFormVersion == null
			   ? VersionOption(template, () => shortFormVersion)
			   : VersionOption(template, () => shortFormVersion, () => longFormVersion);

	// Helper method that adds a version option
	public CommandOption VersionOption(string template, Func<string> shortFormVersionGetter, Func<string>? longFormVersionGetter = null)
	{
		// Version option is special because we stop parsing once we see it
		// So we store it separately for further use
		OptionVersion      = Option(template, "Show version information", CommandOptionType.NoValue);
		ShortVersionGetter = shortFormVersionGetter;
		LongVersionGetter  = longFormVersionGetter ?? shortFormVersionGetter;

		return OptionVersion;
	}

	// Show short hint that reminds users to use help option
	public void ShowHint()
	{
		if (OptionHelp != null)
			Out.WriteLine(string.Format(CultureInfo.CurrentCulture, "Specify --{0} for a list of available options and commands.", OptionHelp.LongName));
	}

	// Show full help
	public int ShowHelp(string? commandName = null)
	{
		for (var cmd = this; cmd != null; cmd = cmd.Parent)
			cmd.IsShowingInformation = true;

		Out.WriteLine(GetHelpText(commandName));

		return 0;
	}

	public string GetHelpText(string? commandName = null)
	{
		var headerBuilder = new StringBuilder("Usage:");
		for (var cmd = this; cmd != null; cmd = cmd.Parent)
			headerBuilder.Insert(6, string.Format(CultureInfo.InvariantCulture, " {0}", cmd.Name));

		CommandLineApplication target;

		if (commandName == null || string.Equals(Name, commandName, StringComparison.OrdinalIgnoreCase))
			target = this;
		else
		{
			target = Commands.SingleOrDefault(cmd => string.Equals(cmd.Name, commandName, StringComparison.OrdinalIgnoreCase))!;

			if (target != null)
				headerBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", commandName);
			else
				// The command name is invalid so don't try to show help for something that doesn't exist
				target = this;
		}

		var optionsBuilder   = new StringBuilder();
		var commandsBuilder  = new StringBuilder();
		var argumentsBuilder = new StringBuilder();

		var arguments = target.Arguments.Where(a => a.ShowInHelpText).ToList();
		arguments.BuildHelp(ref argumentsBuilder, ref headerBuilder);
		
		var options = target.GetOptions().Where(o => o.ShowInHelpText).ToList();
		options.BuildHelp(ref headerBuilder, ref optionsBuilder);

		var commands = target.Commands.Where(c => c.ShowInHelpText).ToList();
		commands.BuildHelp(ref headerBuilder, ref commandsBuilder, ref target, OptionHelp);
		
		if (target.AllowArgumentSeparator)
			headerBuilder.Append(" [[--] <arg>...]");

		headerBuilder.AppendLine();

		var nameAndVersion = new StringBuilder();
		nameAndVersion.AppendLine(GetFullNameAndVersion());
		nameAndVersion.AppendLine();

		return nameAndVersion.ToString() + headerBuilder + argumentsBuilder + optionsBuilder + commandsBuilder + target.ExtendedHelpText;
	}

	public int ShowVersion()
	{
		for (var cmd = this; cmd != null; cmd = cmd.Parent)
			cmd.IsShowingInformation = true;

		Out.WriteLine(FullName);
		Out.WriteLine(LongVersionGetter!());

		return 0;
	}

	public string? GetFullNameAndVersion()
		=> ShortVersionGetter == null
			   ? FullName
			   : string.Format(CultureInfo.InvariantCulture, "{0} {1}", FullName, ShortVersionGetter());

	public void ShowRootCommandFullNameAndVersion()
	{
		var rootCmd = this;
		while (rootCmd.Parent != null)
			rootCmd = rootCmd.Parent;

		Out.WriteLine(rootCmd.GetFullNameAndVersion());
		Out.WriteLine();
	}

	private bool HandleUnexpectedArg(CommandLineApplication command, string[] args, int index, string argTypeName, bool ignoreContinueAfterUnexpectedArg = false)
	{
		if (command._throwOnUnexpectedArg)
		{
			command.ShowHint();
			throw new CommandParsingException(command, $"Unrecognized {argTypeName} '{args[index]}'");
		}

		if (_continueAfterUnexpectedArg && !ignoreContinueAfterUnexpectedArg)
		{
			// Store argument for further use.
			command.RemainingArguments.Add(args[index]);
			return true;
		}

		// Store all remaining arguments for later use.
		command.RemainingArguments.AddRange(new ArraySegment<string>(args, index, args.Length - index));
		return false;
	}
}