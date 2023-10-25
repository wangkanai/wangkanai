// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions.CommandLine;

namespace Wangkanai.CommandLine;

public class CommandOptionTests
{
	[Fact]
	public void OptionsCanBeInherited()
	{
		var    app          = new CommandLineApplication();
		var    optionA      = app.Option("-a|--option-a", "", CommandOptionType.SingleValue, inherited: true);
		string optionAValue = null!;

		app.Option("-b", "", CommandOptionType.SingleValue, inherited: false);

		var subcmd = app.Command("subcmd", c => {
			c.OnExecute(() => {
				optionAValue = optionA.Value()!;
				return 0;
			});
		});

		Assert.Equal(2, app.GetOptions().Count());
		Assert.Single(subcmd.GetOptions());

		app.Execute("-a", "A1", "subcmd");
		Assert.Equal("A1", optionAValue);
		Assert.Throws<CommandParsingException>(() => app.Execute("subcmd", "-b", "B"));
		Assert.Contains("-a|--option-a", subcmd.GetHelpText());
	}

	[Fact]
	public void NestedOptionConflictThrows()
	{
		var app = new CommandLineApplication();
		app.Option("-a|--always", "Top-level", CommandOptionType.SingleValue, inherited: true);
		app.Command("subcmd", c => { c.Option("-a|--ask", "Nested", CommandOptionType.SingleValue); });

		Assert.Throws<InvalidOperationException>(() => app.Execute("subcmd", "-a", "b"));
	}

	[Fact]
	public void OptionsWithSameName()
	{
		var           app    = new CommandLineApplication();
		var           top    = app.Option("-a|--always", "Top-level", CommandOptionType.SingleValue, inherited: false);
		CommandOption nested = null!;
		app.Command("subcmd", c => { nested = c.Option("-a|--ask", "Nested", CommandOptionType.SingleValue); });

		app.Execute("-a", "top");
		Assert.Equal("top", top.Value());
		Assert.Null(nested.Value());

		top.Values.Clear();

		app.Execute("subcmd", "-a", "nested");
		Assert.Null(top.Value());
		Assert.Equal("nested", nested.Value());
	}

	[Fact]
	public void NestedInheritedOptions()
	{
		string globalOptionValue = null!;
		string nest1OptionValue  = null!;
		string nest2OptionValue  = null!;

		CommandLineApplication subcmd2 = null!;

		var app = new CommandLineApplication();
		var g   = app.Option("-g|--global", "Global option", CommandOptionType.SingleValue, inherited: true);
		var subcmd1 = app.Command("lvl1", s1 => {
			var n1 = s1.Option("--nest1", "Nested one level down", CommandOptionType.SingleValue, inherited: true);
			subcmd2 = s1.Command("lvl2", s2 => {
				var n2 = s2.Option("--nest2", "Nested one level down", CommandOptionType.SingleValue, inherited: true);
				s2.HelpOption("-h|--help");
				s2.OnExecute(() => {
					globalOptionValue = g.Value()!;
					nest1OptionValue  = n1.Value()!;
					nest2OptionValue  = n2.Value()!;
					return 0;
				});
			});
		});

		Assert.DoesNotContain(app.GetOptions(), o => o.LongName == "nest2");
		Assert.DoesNotContain(app.GetOptions(), o => o.LongName == "nest1");
		Assert.Contains(app.GetOptions(), o => o.LongName == "global");

		Assert.DoesNotContain(subcmd1.GetOptions(), o => o.LongName == "nest2");
		Assert.Contains(subcmd1.GetOptions(), o => o.LongName == "nest1");
		Assert.Contains(subcmd1.GetOptions(), o => o.LongName == "global");

		Assert.Contains(subcmd2.GetOptions(), o => o.LongName == "nest2");
		Assert.Contains(subcmd2.GetOptions(), o => o.LongName == "nest1");
		Assert.Contains(subcmd2.GetOptions(), o => o.LongName == "global");

		Assert.Throws<CommandParsingException>(() => app.Execute("--nest2", "N2", "--nest1", "N1", "-g", "G"));
		Assert.Throws<CommandParsingException>(() => app.Execute("lvl1", "--nest2", "N2", "--nest1", "N1", "-g", "G"));

		app.Execute("lvl1", "lvl2", "--nest2", "N2", "-g", "G", "--nest1", "N1");
		Assert.Equal("G", globalOptionValue);
		Assert.Equal("N1", nest1OptionValue);
		Assert.Equal("N2", nest2OptionValue);
	}
}
