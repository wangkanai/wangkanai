// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    private readonly bool _treatUnmatchedOptionsAsArguments;

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

    public CommandLineApplication Parent { get; set; }

    public string Name                   { get; set; }
    public string FullName               { get; set; }
    public string Syntax                 { get; set; }
    public string Description            { get; set; }
    public bool   ShowInHelpText         { get; set; } = true;
    public string ExtendedHelpText       { get; set; }
    public bool   IsShowingInformation   { get; protected set; }
    public bool   AllowArgumentSeparator { get; set; }

    public readonly List<CommandLineApplication> Commands;
    public readonly List<CommandArgument>        Arguments;
    public readonly List<CommandOption>          Options;
    public readonly List<string>                 RemainingArguments;

    public CommandOption OptionHelp    { get; private set; }
    public CommandOption OptionVersion { get; private set; }

    public Func<int>    Invoke             { get; set; }
    public Func<string> LongVersionGetter  { get; set; }
    public Func<string> ShortVersionGetter { get; set; }


    public TextWriter Out   { get; set; } = Console.Out;
    public TextWriter Error { get; set; } = Console.Error;

    public IEnumerable<CommandOption> GetOptions()
    {
        var expr     = Options.AsEnumerable();
        var rootNode = this;
        while (rootNode.Parent != null)
        {
            rootNode = rootNode.Parent;
            expr     = expr.Concat(rootNode.Options.Where(o => o.Inherited));
        }

        return expr;
    }

    public CommandLineApplication Command(
        string                         name,
        Action<CommandLineApplication> configuration,
        bool                           throwOnUnexpectedArg = true)
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
        CommandLineApplication       command           = this;
        CommandOption                option            = null;
        IEnumerator<CommandArgument> arguments         = null;
        var                          argumentsAssigned = false;

        for (var index = 0; index < args.Length; index++)
        {
            var arg       = args[index];
            var processed = false;

            if (!processed && option == null)
            {
                string[] longOption  = null;
                string[] shortOption = null;

                // if (arg.StartsWith("--", StringComparison.Ordinal))
                //     longOption = ParseLongOption(arg);
                // else if (arg.StartsWith("-", StringComparison.Ordinal))
                //     shortOption = ParseShortOption(arg);
                //
                
                if (arg.StartsWith("--", StringComparison.Ordinal))
                {
                    longOption = arg.Substring(2).Split(new[] { ':', '=' }, 2);
                }
                else if (arg.StartsWith("-", StringComparison.Ordinal))
                {
                    shortOption = arg.Substring(1).Split(new[] { ':', '=' }, 2);
                }

                if (longOption != null)
                {
                    processed = true;
                    var longOptionName = longOption[0];
                    option = command.GetOptions().SingleOrDefault(opt => string.Equals(opt.LongName, longOptionName, StringComparison.Ordinal));

                    if (option == null && _treatUnmatchedOptionsAsArguments)
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
                            if (string.IsNullOrEmpty(longOptionName) &&
                                !command._throwOnUnexpectedArg       &&
                                AllowArgumentSeparator)
                            {
                                index++;
                                ignoreContinueAfterUnexpectedArg = true;
                            }

                            if (HandleUnexpectedArg(
                                    command,
                                    args,
                                    index,
                                    argTypeName: "option",
                                    ignoreContinueAfterUnexpectedArg))
                            {
                                continue;
                            }

                            break;
                        }

                        if (command.OptionHelp == option)
                            return command.OptionShowHelp();

                        if (command.OptionVersion == option)
                            return command.OptionShowVersion();

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

                    if (option == null && _treatUnmatchedOptionsAsArguments)
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
                            return command.OptionShowHelp();

                        if (command.OptionVersion == option)
                            return command.OptionShowVersion();

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
                command.ProcessOptionThrowException(arg, ref processed, ref option);

            if (!processed && !argumentsAssigned)
                command.ProcessArgumentAssigned(arg, ref processed);

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

    string[] ParseLongOption(string arg)
        => arg.Substring(2).Split(new[] { ':', '=' }, 2);

    string[] ParseShortOption(string arg)
        => arg.Substring(1).Split(new[] { ':', '=' }, 2);


    public CommandOption HelpOption(string template) =>
        OptionHelp = Option(template, "Show help information", CommandOptionType.NoValue);

    public CommandOption VersionOption(string template, string shortFormVersion, string longFormVersion = null) 
        => longFormVersion == null ? 
               VersionOption(template, () => shortFormVersion) : 
               VersionOption(template, () => shortFormVersion, () => longFormVersion);

    public CommandOption VersionOption(
        string       template,
        Func<string> shortFormVersionGetter,
        Func<string> longFormVersionGetter = null)
    {
        OptionVersion      = Option(template, "Show version information", CommandOptionType.NoValue);
        ShortVersionGetter = shortFormVersionGetter;
        LongVersionGetter  = longFormVersionGetter ?? shortFormVersionGetter;

        return OptionVersion;
    }

    public CommandOption Option(string template, string description, CommandOptionType optionType)
        => Option(template, description, optionType, _ => { }, inherited: false);

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

    private bool HandleUnexpectedArg(
        CommandLineApplication command,
        string[]               args,
        int                    index,
        string                 argTypeName,
        bool                   ignoreContinueAfterUnexpectedArg = false)
    {
        if (command._throwOnUnexpectedArg)
        {
            command.ShowHint();
            throw new CommandParsingException(command, $"Unrecognized {argTypeName} '{args[index]}'");
        }
        else if (_continueAfterUnexpectedArg && !ignoreContinueAfterUnexpectedArg)
        {
            command.RemainingArguments.Add(args[index]);
            return true;
        }
        else
        {
            command.RemainingArguments.AddRange(new ArraySegment<string>(args, index, args.Length - index));
            return false;
        }
    }
}