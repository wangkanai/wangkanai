// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            expr     = expr.Concat(rootNode.Options.Where(x => x.Inherited));
        }

        return expr;
    }

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


    public void OnExecute(Func<int> invoke)
        => Invoke = invoke;

    public void OnExecute(Func<Task<int>> invoke)
        => Invoke = () => invoke().Result;

    public int Execute(params string[] args)
    {
        CommandLineApplication    command           = this;
        CommandOption             option            = null;
        CommandArgumentEnumerator arguments         = null;
        var                       argumentsAssigned = false;

        for (var index = 0; index < args.Length; index++)
        {
            var processed = false;
            var arg       = args[index];

            if (!processed && option == null)
            {
                string[] longOption  = null;
                string[] shortOption = null;

                if (arg.StartsWith("--", StringComparison.Ordinal))
                    longOption = ParseLongOption(arg);
                else if (arg.StartsWith("-", StringComparison.Ordinal))
                    shortOption = ParseShortOption(arg);

                if (longOption != null)
                {
                    processed = true;
                    var longOptionName = longOption[0];
                    option = command.GetOptions().SingleOrDefault(o => string.Equals(o.LongName, longOptionName, StringComparison.Ordinal));

                    if (option == null && _treadUnmatchedOptionsAsArguments)
                    {
                        if (arguments == null)
                            arguments = new CommandArgumentEnumerator(command.Arguments.GetEnumerator());
                        if (arguments.MoveNext())
                        {
                            processed = true;
                            arguments.Current.Values.Add(arg);
                            argumentsAssigned = true;
                            continue;
                        }

                        if (option == null)
                        {
                            var ignoreContinueAfterUnexpectedArg = false;
                            if (string.IsNullOrEmpty(longOptionName) && !command._throwOnUnexpectedArg && AllowArgumentSeparator)
                            {
                                index++;
                                ignoreContinueAfterUnexpectedArg = true;
                            }

                            if (HandleUnexpectedArg(command, args, index, argTypeName: "option", ignoreContinueAfterUnexpectedArg))
                                continue;

                            break;
                        }

                        if (command.OptionHelp == option)
                        {
                            command.ShowHelp();
                            return 0;
                        }

                        if (command.OptionVersion == option)
                        {
                            command.ShowVersion();
                            return 0;
                        }

                        if (longOption.Length == 2)
                        {
                            if (!option.TryParse(longOption[1]))
                            {
                                command.ShowHelp();
                                throw new CommandParsingException(command, $"Unexpected value '{longOption[1]}' for option '{option.LongName}'");
                            }

                            option = null;
                        }
                        else if (option.OptionType == CommandOptionType.NoValue)
                        {
                            option.TryParse(null);
                            option = null;
                        }
                    }
                }

                if (shortOption != null)
                {
                    processed = true;
                    option    = command.GetOptions().SingleOrDefault(o => string.Equals(o.ShortName, shortOption[0], StringComparison.Ordinal));

                    if (option == null && _treadUnmatchedOptionsAsArguments)
                    {
                        if (arguments == null)
                            arguments = new CommandArgumentEnumerator(command.Arguments.GetEnumerator());
                        if (arguments.MoveNext())
                        {
                            processed = true;
                            arguments.Current.Values.Add(arg);
                            argumentsAssigned = true;
                            continue;
                        }

                        if (option == null)
                            option = command.GetOptions().SingleOrDefault(o => string.Equals(o.SymbolName, shortOption[0], StringComparison.Ordinal));

                        if (option == null)
                        {
                            if (HandleUnexpectedArg(command, args, index, argTypeName: "option"))
                                continue;
                            break;
                        }

                        if (command.OptionHelp == option)
                        {
                            command.ShowHelp();
                            return 0;
                        }
                        else if (command.OptionVersion == option)
                        {
                            command.ShowVersion();
                            return 0;
                        }

                        if (shortOption.Length == 2)
                        {
                            if (!option.TryParse(shortOption[1]))
                            {
                                command.ShowHint();
                                throw new CommandParsingException(command, $"Unexpected value '{shortOption[1]}' for option '{option.LongName}'");
                            }

                            option = null;
                        }
                        else if (option.OptionType == CommandOptionType.NoValue)
                        {
                            option.TryParse(null);
                            option = null;
                        }
                    }
                }
            }

            if (!processed && option != null)
            {
                processed = true;
                if (!option.TryParse(arg))
                {
                    command.ShowHint();
                    throw new CommandParsingException(command, $"Unexpected value '{arg}' for option '{option.LongName}'");
                }

                option = null;
            }

            if (!processed && argumentsAssigned)
            {
                var currentCommand = command;
                foreach (var subcommand in command.Commands)
                    if (string.Equals(subcommand.Name, arg, StringComparison.Ordinal))
                    {
                        processed = true;
                        command   = subcommand;
                        break;
                    }

                if (command != currentCommand)
                    processed = true;
            }

            if (!processed)
            {
                if (HandleUnexpectedArg(command, args, index, argTypeName: "command or argument"))
                    continue;
                break;
            }
        }

        if (option != null)
        {
            command.ShowHint();
            throw new CommandParsingException(command, $"Missing value for option '{option.LongName}'");
        }

        return command.Invoke();

        string[] ParseLongOption(string arg)
            => arg.Substring(2).Split(new[] { ':', '=' }, 2);

        string[] ParseShortOption(string arg)
            => arg.Substring(1).Split(new[] { ':', '=' }, 2);
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
        => Argument(name, description, _ => { }, multipleValues);

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

    public void ShowHint()
    {
        if (OptionHelp != null)
            Out.WriteLine(string.Format(CultureInfo.CurrentCulture, "Specify --{0} for a list of available options and commands.", OptionHelp.LongName));
    }

    public void ShowHelp(string commandName = null)
    {
        for (var cmd = this; cmd != null; cmd = cmd.Parent)
        {
            cmd.IsShowingInformation = true;
        }

        Out.WriteLine(GetHelpText(commandName));
    }

    public void ShowVersion()
    {
        for (var cmd = this; cmd != null; cmd = cmd.Parent)
            cmd.IsShowingInformation = true;

        Out.WriteLine(FullName);
        Out.WriteLine(LongVersionGetter());
    }

    public string GetFullNameAndVersion()
        => ShortVersionGetter == null ? FullName : string.Format(CultureInfo.InvariantCulture, "{0} {1}", FullName, ShortVersionGetter());

    public virtual string GetHelpText(string commandName = null)
    {
        var headerBuilder = new StringBuilder("Usage:");
        for (var cmd = this; cmd != null; cmd = cmd.Parent)
            headerBuilder.Insert(6, string.Format(CultureInfo.InvariantCulture, " {0}", cmd.Name));

        CommandLineApplication target;
        if (commandName == null || string.Equals(Name, commandName, StringComparison.Ordinal))
            target = this;
        else
        {
            target = Commands.SingleOrDefault(cmd => string.Equals(cmd.Name, commandName, StringComparison.OrdinalIgnoreCase));

            if (target != null)
                headerBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", commandName);
            else
                target = this;
        }

        var optionsBuilder   = new StringBuilder();
        var commandsBuilder  = new StringBuilder();
        var argumentsBuilder = new StringBuilder();

        var arguments = target.Arguments.Where(x => x.ShowInHelpText).ToList();

        if (arguments.Any())
        {
            headerBuilder.Append(" [arguments]");

            argumentsBuilder.AppendLine();
            argumentsBuilder.AppendLine("Arguments:");
            var maxArgLength = arguments.Max(x => x.Name.Length);
            var outputFormat = string.Format(CultureInfo.InvariantCulture, " {{0, -{0}}}{{1}}", maxArgLength + 2);
            foreach (var arg in arguments)
            {
                argumentsBuilder.AppendFormat(CultureInfo.InvariantCulture, outputFormat, arg.Name, arg.Description);
                argumentsBuilder.AppendLine();
            }
        }

        var options = target.GetOptions().Where(x => x.ShowInHelpText).ToList();
        if (options.Any())
        {
            headerBuilder.Append(" [options]");

            optionsBuilder.AppendLine();
            optionsBuilder.AppendLine("Options:");
            var maxOptLength = options.Max(x => x.Template.Length);
            var outputFormat = string.Format(CultureInfo.InvariantCulture, " {{0, -{0}}}{{1}}", maxOptLength + 2);
            foreach (var opt in options)
            {
                optionsBuilder.AppendFormat(CultureInfo.InvariantCulture, outputFormat, opt.Template, opt.Description);
                optionsBuilder.AppendLine();
            }

            if (OptionHelp != null)
            {
                commandsBuilder.AppendLine();
                commandsBuilder.Append($"Use \"{target.Name} [command] --{OptionHelp.LongName}\" for more information about a command.");
                commandsBuilder.AppendLine();
            }
        }

        if (target.AllowArgumentSeparator)
            headerBuilder.Append(" [[--] <arg>...]");

        headerBuilder.AppendLine();

        var nameAndVersion = new StringBuilder();
        nameAndVersion.AppendLine(GetFullNameAndVersion());
        nameAndVersion.AppendLine();

        return nameAndVersion.ToString()   +
               headerBuilder.ToString()    +
               argumentsBuilder.ToString() +
               optionsBuilder.ToString()   +
               commandsBuilder.ToString()  +
               target.ExtendedHelpText;
    }

    private bool HandleUnexpectedArg(CommandLineApplication command, string[] args, int index, string argTypeName, bool ignoreContinueAfterUnexpecting = false)
    {
        if (command._throwOnUnexpectedArg)
        {
            command.ShowHint();
            throw new CommandParsingException(command, $"Unrecognized {argTypeName} '{args[index]}'");
        }
        else if (_continueAfterUnexpectedArg && !ignoreContinueAfterUnexpecting)
        {
            command.RemainingArgements.Add(args[index]);
            return true;
        }
        else
        {
            command.RemainingArgements.AddRange(new ArraySegment<string>(args, index, args.Length - index));
            return false;
        }
    }
}