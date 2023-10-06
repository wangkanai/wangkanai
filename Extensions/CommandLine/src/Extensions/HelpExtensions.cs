// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Globalization;
using System.Text;

namespace Wangkanai.Extensions.CommandLine;

public static class HelpExtensions
{
	internal static void BuildHelp(this List<CommandArgument> arguments, ref StringBuilder headerBuilder, ref StringBuilder argumentsBuilder)
	{
		if (arguments.Count == 0)
			return;

		headerBuilder.Append(" [arguments]");

		argumentsBuilder.AppendLine();
		argumentsBuilder.AppendLine("Arguments:");
		var maxArgLen    = arguments.Max(a => a.Name!.Length);
		var outputFormat = string.Format(CultureInfo.InvariantCulture, "  {{0, -{0}}}{{1}}", maxArgLen + 2);
		foreach (var arg in arguments)
		{
			argumentsBuilder.AppendFormat(CultureInfo.InvariantCulture, outputFormat, arg.Name, arg.Description);
			argumentsBuilder.AppendLine();
		}
	}

	internal static void BuildHelp(this List<CommandOption> options, ref StringBuilder headerBuilder, ref StringBuilder optionsBuilder)
	{
		if (!options.Any()) 
			return;
		
		headerBuilder.Append(" [options]");

		optionsBuilder.AppendLine();
		optionsBuilder.AppendLine("Options:");
		var maxOptLen    = options.Max(o => o.Template.Length);
		var outputFormat = string.Format(CultureInfo.InvariantCulture, "  {{0, -{0}}}{{1}}", maxOptLen + 2);
		foreach (var opt in options)
		{
			optionsBuilder.AppendFormat(CultureInfo.InvariantCulture, outputFormat, opt.Template, opt.Description);
			optionsBuilder.AppendLine();
		}
	}

	internal static void BuildHelp(this List<CommandLineApplication> commands, ref StringBuilder headerBuilder, ref StringBuilder commandsBuilder, ref CommandLineApplication target, CommandOption? optionHelp)
	{
		if (!commands.Any()) 
			return;
		
		headerBuilder.Append(" [command]");

		commandsBuilder.AppendLine();
		commandsBuilder.AppendLine("Commands:");
		var maxCmdLen    = commands.Max(c => c.Name!.Length);
		var outputFormat = string.Format(CultureInfo.InvariantCulture, "  {{0, -{0}}}{{1}}", maxCmdLen + 2);
		foreach (var cmd in commands.OrderBy(c => c.Name))
		{
			commandsBuilder.AppendFormat(CultureInfo.InvariantCulture, outputFormat, cmd.Name, cmd.Description);
			commandsBuilder.AppendLine();
		}

		if (optionHelp != null)
		{
			var output = $"Use \"{target.Name} [command] --{optionHelp.LongName}\" for more information about a command.";
			commandsBuilder.AppendLine();
			commandsBuilder.Append(output);
			commandsBuilder.AppendLine();
		}
	}
}