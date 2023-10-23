// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

#pragma warning disable CA1861
public class CommandArgumentTests
{
	[Theory]
	[InlineData(new string[0], new string[0], null)]
	[InlineData(new[] { "--" }, new string[0], null)]
	[InlineData(new[] { "-t", "val" }, new string[0], "val")]
	[InlineData(new[] { "-t", "val", "--" }, new string[0], "val")]
	[InlineData(new[] { "--top", "val", "--", "a" }, new[] { "a" }, "val")]
	[InlineData(new[] { "--", "a", "--top", "val" }, new[] { "a", "--top", "val" }, null)]
	[InlineData(new[] { "-t", "val", "--", "a", "--", "b" }, new[] { "a", "--", "b" }, "val")]
	[InlineData(new[] { "--", "--help" }, new[] { "--help" }, null)]
	[InlineData(new[] { "--", "--version" }, new[] { "--version" }, null)]
	public void ArgumentSeparator(string[] input, string[] expectedRemaining, string topLevelValue)
	{
		var app = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.AllowArgumentSeparator = true;
		var optHelp    = app.HelpOption("--help");
		var optVersion = app.VersionOption("--version", "1", "1.0");
		var optTop     = app.Option("-t|--top <TOP>", "arg for command", CommandOptionType.SingleValue);
		app.Execute(input);

		Assert.Equal(topLevelValue, optTop.Value());
		Assert.False(optHelp.HasValue());
		Assert.False(optVersion.HasValue());
		Assert.Equal(expectedRemaining, app.RemainingArguments.ToArray());
	}

	[Theory]
	[InlineData(new string[0], new string[0], null, false)]
	[InlineData(new[] { "--" }, new[] { "--" }, null, false)]
	[InlineData(new[] { "-t", "val" }, new string[0], "val", false)]
	[InlineData(new[] { "-t", "val", "--" }, new[] { "--" }, "val", false)]
	[InlineData(new[] { "--top", "val", "--", "a" }, new[] { "--", "a" }, "val", false)]
	[InlineData(new[] { "-t", "val", "--", "a", "--", "b" }, new[] { "--", "a", "--", "b" }, "val", false)]
	[InlineData(new[] { "--help", "--" }, new string[0], null, true)]
	[InlineData(new[] { "--version", "--" }, new string[0], null, true)]
	public void ArgumentSeparator_TreatedAsUnexpected(string[] input, string[] expectedRemaining, string topLevelValue, bool isShowingInformation)
	{
		var app        = new CommandLineApplication(throwOnUnexpectedArg: false);
		var optHelp    = app.HelpOption("--help");
		var optVersion = app.VersionOption("--version", "1", "1.0");
		var optTop     = app.Option("-t|--top <TOP>", "arg for command", CommandOptionType.SingleValue);

		app.Execute(input);

		Assert.Equal(topLevelValue, optTop.Value());
		Assert.Equal(expectedRemaining, app.RemainingArguments.ToArray());
		Assert.Equal(isShowingInformation, app.IsShowingInformation);

		// Help and Version options never get values; parsing ends when encountered.
		Assert.False(optHelp.HasValue());
		Assert.False(optVersion.HasValue());
	}

	[Theory]
	[InlineData(new[] { "--", "a", "--top", "val" }, new[] { "--", "a", "--top", "val" }, null, false)]
	[InlineData(new[] { "--", "--help" }, new[] { "--", "--help" }, null, false)]
	[InlineData(new[] { "--", "--version" }, new[] { "--", "--version" }, null, false)]
	[InlineData(new[] { "unexpected", "--", "--version" }, new[] { "unexpected", "--", "--version" }, null, false)]
	public void ArgumentSeparator_TreatedAsUnexpected_Default(string[] input, string[] expectedRemaining, string topLevelValue, bool isShowingInformation)
	{
		var app        = new CommandLineApplication(throwOnUnexpectedArg: false);
		var optHelp    = app.HelpOption("--help");
		var optVersion = app.VersionOption("--version", "1", "1.0");
		var optTop     = app.Option("-t|--top <TOP>", "arg for command", CommandOptionType.SingleValue);

		app.Execute(input);

		Assert.Equal(topLevelValue, optTop.Value());
		Assert.Equal(expectedRemaining, app.RemainingArguments.ToArray());
		Assert.Equal(isShowingInformation, app.IsShowingInformation);

		// Help and Version options never get values; parsing ends when encountered.
		Assert.False(optHelp.HasValue());
		Assert.False(optVersion.HasValue());
	}

	[Theory]
	[InlineData(new[] { "--", "a", "--top", "val" }, new[] { "--", "a" }, "val", false)]
	[InlineData(new[] { "--", "--help" }, new[] { "--" }, null, true)]
	[InlineData(new[] { "--", "--version" }, new[] { "--" }, null, true)]
	[InlineData(new[] { "unexpected", "--", "--version" }, new[] { "unexpected", "--" }, null, true)]
	public void ArgumentSeparator_TreatedAsUnexpected_Continue(string[] input, string[] expectedRemaining, string topLevelValue, bool isShowingInformation)
	{
		var app        = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var optHelp    = app.HelpOption("--help");
		var optVersion = app.VersionOption("--version", "1", "1.0");
		var optTop     = app.Option("-t|--top <TOP>", "arg for command", CommandOptionType.SingleValue);

		app.Execute(input);

		Assert.Equal(topLevelValue, optTop.Value());
		Assert.Equal(expectedRemaining, app.RemainingArguments.ToArray());
		Assert.Equal(isShowingInformation, app.IsShowingInformation);

		// Help and Version options never get values; parsing ends when encountered.
		Assert.False(optHelp.HasValue());
		Assert.False(optVersion.HasValue());
	}

	[Fact]
	public void HelpTextIgnoresHiddenItems()
	{
		var app = new CommandLineApplication
		{
			Name        = "ninja-app",
			Description = "You can't see it until it is too late"
		};

		app.Command("star", c => {
			c.Option("--points <p>", "How many", CommandOptionType.MultipleValue);
			c.ShowInHelpText = false;
		});
		app.Option("--smile", "Be a nice ninja", CommandOptionType.NoValue, o => { o.ShowInHelpText = false; });

		var a = app.Argument("name", "Pseudonym, of course");
		a.ShowInHelpText = false;

		var help = app.GetHelpText();

		Assert.Contains("ninja-app", help);
		Assert.DoesNotContain("--points", help);
		Assert.DoesNotContain("--smile", help);
		Assert.DoesNotContain("name", help);
	}

	[Fact]
	public void HelpTextUsesHelpOptionName()
	{
		var app = new CommandLineApplication();
		app.Name = "homebrew";

		app.HelpOption("--ayuda-me");
		var help = app.GetHelpText();
		Assert.Contains("--ayuda-me", help);
	}

	[Fact]
	public void HelpTextShowsArgSeparator()
	{
		var app = new CommandLineApplication(throwOnUnexpectedArg: false)
		{
			Name                   = "proxy-command",
			AllowArgumentSeparator = true
		};
		app.HelpOption("-h|--help");
		Assert.Contains("Usage: proxy-command [options] [[--] <arg>...]", app.GetHelpText());
	}

	[Fact]
	public void HelpTextShowsExtendedHelp()
	{
		var app = new CommandLineApplication()
		{
			Name = "befuddle",
			ExtendedHelpText = @"
Remarks:
  This command is so confusing that I want to include examples in the help text.

Examples:
  dotnet befuddle -- I Can Haz Confusion Arguments
"
		};

		Assert.Contains(app.ExtendedHelpText, app.GetHelpText());
	}

	[Theory]
	[InlineData(new[] { "--version", "--flag" }, "1.0")]
	[InlineData(new[] { "-V", "-f" }, "1.0")]
	[InlineData(new[] { "--help", "--flag" }, "some flag")]
	[InlineData(new[] { "-h", "-f" }, "some flag")]
	public void HelpAndVersionOptionStopProcessing(string[] input, string expectedOutData)
	{
		using var outWriter = new StringWriter();
		var       app       = new CommandLineApplication { Out = outWriter };
		app.HelpOption("-h --help");
		app.VersionOption("-V --version", "1", "1.0");
		var optFlag = app.Option("-f |--flag", "some flag", CommandOptionType.NoValue);

		app.Execute(input);

		outWriter.Flush();
		var outData = outWriter.ToString();
		Assert.Contains(expectedOutData, outData);
		Assert.False(optFlag.HasValue());
	}

	// disable inaccurate analyzer error https://github.com/xunit/xunit/issues/1274
	[Theory]
	[InlineData("-f:File1", "-f:File2")]
	[InlineData("--file:File1", "--file:File2")]
	[InlineData("--file", "File1", "--file", "File2")]
	public void ThrowsExceptionOnSingleValueOptionHavingTwoValues(params string[] inputOptions)
	{
		var app = new CommandLineApplication();
		app.Option("-f |--file", "some file", CommandOptionType.SingleValue);
		var exception = Assert.Throws<CommandParsingException>(() => app.Execute(inputOptions));

		Assert.Equal("Unexpected value 'File2' for option 'file'", exception.Message);
	}

	[Theory]
	[InlineData("-v")]
	[InlineData("--verbose")]
	public void NoValueOptionCanBeSet(string input)
	{
		var app        = new CommandLineApplication();
		var optVerbose = app.Option("-v |--verbose", "be verbose", CommandOptionType.NoValue);
		app.Execute(input);

		Assert.True(optVerbose.HasValue());
	}

	[Theory]
	[InlineData("-v:true")]
	[InlineData("--verbose:true")]
	public void ThrowsExceptionOnNoValueOptionHavingValue(string inputOption)
	{
		var app = new CommandLineApplication();
		app.Option("-v |--verbose", "be verbose", CommandOptionType.NoValue);
		var exception = Assert.Throws<CommandParsingException>(() => app.Execute(inputOption));

		Assert.Equal("Unexpected value 'true' for option 'verbose'", exception.Message);
	}

	[Fact]
	public void ThrowsExceptionOnEmptyCommandOrArgument()
	{
		var inputOption = String.Empty;
		var app         = new CommandLineApplication();
		var exception   = Assert.Throws<CommandParsingException>(() => app.Execute(inputOption));

		Assert.Equal($"Unrecognized command or argument '{inputOption}'", exception.Message);
	}

	[Fact]
	public void ThrowsExceptionOnInvalidOption()
	{
		var inputOption = "-";
		var app         = new CommandLineApplication();

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute(inputOption));

		Assert.Equal($"Unrecognized option '{inputOption}'", exception.Message);
	}

	[Fact]
	public void TreatUnmatchedOptionsAsArguments()
	{
		CommandArgument first        = null!;
		CommandArgument second       = null!;
		CommandOption   firstOption  = null!;
		CommandOption   secondOption = null!;

		var firstUnmatchedOption = "-firstUnmatchedOption";
		var firstActualOption    = "-firstActualOption";
		var seconUnmatchedOption = "--secondUnmatchedOption";
		var secondActualOption   = "--secondActualOption";

		var app = new CommandLineApplication(treatUnmatchedOptionsAsArguments: true);

		app.Command("test", c => {
			firstOption  = c.Option("-firstActualOption", "first option", CommandOptionType.NoValue);
			secondOption = c.Option("--secondActualOption", "second option", CommandOptionType.NoValue);

			first  = c.Argument("first", "First argument");
			second = c.Argument("second", "Second argument");
			c.OnExecute(() => 0);
		});

		app.Execute("test", firstUnmatchedOption, firstActualOption, seconUnmatchedOption, secondActualOption);

		Assert.Equal(firstUnmatchedOption, first.Value);
		Assert.Equal(seconUnmatchedOption, second.Value);

		Assert.Equal(firstActualOption, firstOption.Template);
		Assert.Equal(secondActualOption, secondOption.Template);
	}

	[Fact]
	public void ThrowExceptionWhenUnmatchedOptionAndTreatUnmatchedOptionsAsArgumentsIsFalse()
	{
		CommandArgument first = null!;

		var firstOption = "-firstUnmatchedOption";
		var app         = new CommandLineApplication(treatUnmatchedOptionsAsArguments: false);
		app.Command("test", c => {
			first = c.Argument("first", "First argument");
			c.OnExecute(() => 0);
		});

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("test", firstOption));

		Assert.Equal($"Unrecognized option '{firstOption}'", exception.Message);
	}
}
#pragma warning restore CA1861
