// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

public static class OptionExtensions
{
	public static IEnumerable<CommandOption> GetOptions(this CommandLineApplication command)
	{
		var rootNode = command;
		var expr     = command.Options.AsEnumerable();

		while (rootNode.Parent != null)
		{
			rootNode = rootNode.Parent;
			expr     = expr.Concat(rootNode.Options.Where(o => o.Inherited));
		}

		return expr;
	}
}