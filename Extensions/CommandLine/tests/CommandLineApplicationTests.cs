// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions.CommandLine;

namespace Wangkanai.CommandLine;

public class CommandLineApplicationTests
{
	[Fact]
	public void CommandNameCanBeMatched()
	{
		var called = false;

		var app = new CommandLineApplication();
		app.Command("test", c => {
			c.OnExecute(() => {
				called = true;
				return 5;
			});
		});

		var result = app.Execute("test");
		Assert.Equal(5, result);
		Assert.True(called);
	}

	[Fact]
	public void RemainingArgsArePassed()
	{
		CommandArgument? first  = null;
		CommandArgument? second = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Argument("first", "First argument");
			second = c.Argument("second", "Second argument");
			c.OnExecute(() => 0);
		});

		app.Execute("test", "one", "two");

		Assert.Equal("one", first!.Value);
		Assert.Equal("two", second!.Value);
	}

	[Fact]
	public void ExtraArgumentCausesException()
	{
		CommandArgument? first  = null;
		CommandArgument? second = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Argument("first", "First argument");
			second = c.Argument("second", "Second argument");
			c.OnExecute(() => 0);
		});

		var ex = Assert.Throws<CommandParsingException>(() => app.Execute("test", "one", "two", "three"));

		Assert.Contains("three", ex.Message);
	}

	[Fact]
	public void ExtraArgumentAddedToRemaining()
	{
		CommandArgument? first  = null;
		CommandArgument? second = null;

		var app = new CommandLineApplication();

		var testCommand = app.Command("test", c => {
				first  = c.Argument("first", "First argument");
				second = c.Argument("second", "Second argument");
				c.OnExecute(() => 0);
			},
			throwOnUnexpectedArg: false);

		app.Execute("test", "one", "two", "three");

		Assert.Equal("one", first!.Value);
		Assert.Equal("two", second!.Value);
		var remaining = Assert.Single(testCommand.RemainingArguments);
		Assert.Equal("three", remaining);
	}

	[Fact]
	public void UnknownCommandCausesException()
	{
		var app = new CommandLineApplication();

		app.Command("test", c => {
			c.Argument("first", "First argument");
			c.Argument("second", "Second argument");
			c.OnExecute(() => 0);
		});

		var ex = Assert.Throws<CommandParsingException>(() => app.Execute("test2", "one", "two", "three"));

		Assert.Contains("test2", ex.Message);
	}

	private static readonly string[] word_one_five   = new[] { "one", "two", "three", "four", "five" };
	private static readonly string[] word_three_five = new[] { "three", "four", "five" };

	[Fact]
	public void MultipleValuesArgumentConsumesAllArgumentValues()
	{
		CommandArgument? argument = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			argument = c.Argument("arg", "Argument that allows multiple values", multipleValues: true);
			c.OnExecute(() => 0);
		});

		app.Execute("test", "one", "two", "three", "four", "five");

		Assert.Equal(word_one_five, argument!.Values);
	}

	[Fact]
	public void MultipleValuesArgumentConsumesAllRemainingArgumentValues()
	{
		CommandArgument? first  = null;
		CommandArgument? second = null;
		CommandArgument? third  = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Argument("first", "First argument");
			second = c.Argument("second", "Second argument");
			third  = c.Argument("third", "Third argument that allows multiple values", multipleValues: true);
			c.OnExecute(() => 0);
		});

		app.Execute("test", "one", "two", "three", "four", "five");

		Assert.Equal("one", first!.Value);
		Assert.Equal("two", second!.Value);
		Assert.Equal(word_three_five, third!.Values);
	}

	[Fact]
	public void MultipleValuesArgumentMustBeTheLastArgument()
	{
		var app = new CommandLineApplication();
		app.Argument("first", "First argument", multipleValues: true);
		var ex = Assert.Throws<InvalidOperationException>(() => app.Argument("second", "Second argument"));

		Assert.Contains($"The last argument 'first' accepts multiple values. No more argument can be added.", ex.Message);
	}

	[Fact]
	public void OptionSwitchMayBeProvided()
	{
		CommandOption first  = null;
		CommandOption second = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Option("--first <NAME>", "First argument", CommandOptionType.SingleValue);
			second = c.Option("--second <NAME>", "Second argument", CommandOptionType.SingleValue);
			c.OnExecute(() => 0);
		});

		app.Execute("test", "--first", "one", "--second", "two");

		Assert.Equal("one", first.Values[0]);
		Assert.Equal("two", second.Values[0]);
	}

	[Fact]
	public void OptionValueMustBeProvided()
	{
		CommandOption first = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first = c.Option("--first <NAME>", "First argument", CommandOptionType.SingleValue);
			c.OnExecute(() => 0);
		});

		var ex = Assert.Throws<CommandParsingException>(() => app.Execute("test", "--first"));

		Assert.Contains($"Missing value for option '{first.LongName}'", ex.Message);
	}

	[Fact]
	public void ValuesMayBeAttachedToSwitch()
	{
		CommandOption first  = null;
		CommandOption second = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Option("--first <NAME>", "First argument", CommandOptionType.SingleValue);
			second = c.Option("--second <NAME>", "Second argument", CommandOptionType.SingleValue);
			c.OnExecute(() => 0);
		});

		app.Execute("test", "--first=one", "--second:two");

		Assert.Equal("one", first.Values[0]);
		Assert.Equal("two", second.Values[0]);
	}

	[Fact]
	public void ShortNamesMayBeDefined()
	{
		CommandOption first  = null;
		CommandOption second = null;

		var app = new CommandLineApplication();

		app.Command("test", c => {
			first  = c.Option("-1 --first <NAME>", "First argument", CommandOptionType.SingleValue);
			second = c.Option("-2 --second <NAME>", "Second argument", CommandOptionType.SingleValue);
			c.OnExecute(() => 0);
		});

		app.Execute("test", "-1=one", "-2", "two");

		Assert.Equal("one", first.Values[0]);
		Assert.Equal("two", second.Values[0]);
	}

	[Fact]
	public void ThrowsExceptionOnUnexpectedCommandOrArgumentByDefault()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication();

		app.Command("test", c => { c.OnExecute(() => 0); });

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("test", unexpectedArg));
		Assert.Equal($"Unrecognized command or argument '{unexpectedArg}'", exception.Message);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgument()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication();

		var testCmd = app.Command("test", c => { c.OnExecute(() => 0); },
			throwOnUnexpectedArg: false);

		// (does not throw)
		app.Execute("test", unexpectedArg);
		var arg = Assert.Single(testCmd.RemainingArguments);
		Assert.Equal(unexpectedArg, arg);
	}

	[Fact]
	public void AllowArgumentBeforeNoValueOption()
	{
		var app      = new CommandLineApplication();
		var argument = app.Argument("first", "first argument");
		var option   = app.Option("--first", "first option", CommandOptionType.NoValue);

		app.Execute("one", "--first");

		Assert.Equal("one", argument.Value);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowArgumentAfterNoValueOption()
	{
		var app      = new CommandLineApplication();
		var argument = app.Argument("first", "first argument");
		var option   = app.Option("--first", "first option", CommandOptionType.NoValue);

		app.Execute("--first", "one");

		Assert.Equal("one", argument.Value);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowArgumentBeforeSingleValueOption()
	{
		var app      = new CommandLineApplication();
		var argument = app.Argument("first", "first argument");
		var option   = app.Option("--first <value>", "first option", CommandOptionType.SingleValue);

		app.Execute("one", "--first", "two");

		Assert.Equal("one", argument.Value);
		Assert.Equal("two", option.Value());
	}

	[Fact]
	public void AllowArgumentAfterSingleValueOption()
	{
		var app      = new CommandLineApplication();
		var argument = app.Argument("first", "first argument");
		var option   = app.Option("--first <value>", "first option", CommandOptionType.SingleValue);

		app.Execute("--first", "one", "two");

		Assert.Equal("two", argument.Value);
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentBeforeNoValueOption_Default()
	{
		var arguments = new[] { "UnexpectedArg", "--first" };
		var app       = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option    = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(arguments);

		Assert.Equal(arguments, app.RemainingArguments.ToArray());
		Assert.False(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentBeforeNoValueOption_Continue()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option        = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(unexpectedArg, "--first");

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedArg, arg);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentAfterNoValueOption()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option        = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute("--first", unexpectedArg);

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedArg, arg);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentBeforeSingleValueOption_Default()
	{
		var arguments = new[] { "UnexpectedArg", "--first", "one" };
		var app       = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(arguments);

		Assert.Equal(arguments, app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentBeforeSingleValueOption_Continue()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option        = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(unexpectedArg, "--first", "one");

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedArg, arg);
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedArgumentAfterSingleValueOption()
	{
		var unexpectedArg = "UnexpectedArg";
		var app           = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option        = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute("--first", "one", unexpectedArg);

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedArg, arg);
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void ThrowsExceptionOnUnexpectedLongOptionByDefault()
	{
		var unexpectedOption = "--UnexpectedOption";
		var app              = new CommandLineApplication();

		app.Command("test", c => { c.OnExecute(() => 0); });

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("test", unexpectedOption));
		Assert.Equal($"Unrecognized option '{unexpectedOption}'", exception.Message);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOption()
	{
		var unexpectedOption = "--UnexpectedOption";
		var app              = new CommandLineApplication();

		var testCmd = app.Command("test", c => { c.OnExecute(() => 0); },
			throwOnUnexpectedArg: false);

		// (does not throw)
		app.Execute("test", unexpectedOption);
		var arg = Assert.Single(testCmd.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionBeforeNoValueOption_Default()
	{
		var arguments = new[] { "--unexpected", "--first" };
		var app       = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(arguments);

		Assert.Equal(arguments, app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionBeforeNoValueOption_Continue()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option           = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(unexpectedOption, "--first");

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionAfterNoValueOption()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option           = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute("--first", unexpectedOption);

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionBeforeSingleValueOption_Default()
	{
		var arguments = new[] { "--unexpected", "--first", "one" };
		var app       = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(arguments);

		Assert.Equal(arguments, app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionBeforeSingleValueOption_Continue()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option           = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(unexpectedOption, "--first", "one");

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionAfterSingleValueOption()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option           = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute("--first", "one", unexpectedOption);

		var arg = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueBeforeNoValueOption_Default()
	{
		var arguments = new[] { "--unexpected", "value", "--first" };
		var app       = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(arguments);

		Assert.Equal(arguments, app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueBeforeNoValueOption_Continue()
	{
		var unexpectedOption = "--unexpected";
		var unexpectedValue  = "value";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option           = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute(unexpectedOption, unexpectedValue, "--first");

		Assert.Equal(new[] { unexpectedOption, unexpectedValue }, app.RemainingArguments.ToArray());
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueAfterNoValueOption()
	{
		var unexpectedOption = "--unexpected";
		var unexpectedValue  = "value";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option           = app.Option("--first", "first option", CommandOptionType.NoValue);

		// (does not throw)
		app.Execute("--first", unexpectedOption, unexpectedValue);

		Assert.Equal(new[] { unexpectedOption, unexpectedValue }, app.RemainingArguments.ToArray());
		Assert.True(option.HasValue());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueBeforeSingleValueOption_Default()
	{
		var unexpectedOption = "--unexpected";
		var unexpectedValue  = "value";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false);
		app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(unexpectedOption, unexpectedValue, "--first", "one");

		Assert.Equal(
			new[] { unexpectedOption, unexpectedValue, "--first", "one" },
			app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueBeforeSingleValueOption_Continue()
	{
		var unexpectedOption = "--unexpected";
		var unexpectedValue  = "value";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var option           = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute(unexpectedOption, unexpectedValue, "--first", "one");

		Assert.Equal(
			new[] { unexpectedOption, unexpectedValue },
			app.RemainingArguments.ToArray());
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedLongOptionWithValueAfterSingleValueOption()
	{
		var unexpectedOption = "--unexpected";
		var unexpectedValue  = "value";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false);
		var option           = app.Option("--first", "first option", CommandOptionType.SingleValue);

		// (does not throw)
		app.Execute("--first", "one", unexpectedOption, unexpectedValue);

		Assert.Equal(new[] { unexpectedOption, unexpectedValue }, app.RemainingArguments.ToArray());
		Assert.Equal("one", option.Value());
	}

	[Fact]
	public void ThrowsExceptionOnUnexpectedShortOptionByDefault()
	{
		var unexpectedOption = "-uexp";
		var app              = new CommandLineApplication();

		app.Command("test", c => { c.OnExecute(() => 0); });

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("test", unexpectedOption));
		Assert.Equal($"Unrecognized option '{unexpectedOption}'", exception.Message);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedShortOption()
	{
		var unexpectedOption = "-uexp";
		var app              = new CommandLineApplication();

		var testCmd = app.Command("test", c => { c.OnExecute(() => 0); },
			throwOnUnexpectedArg: false);

		// (does not throw)
		app.Execute("test", unexpectedOption);
		var arg = Assert.Single(testCmd.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
	}

	[Fact]
	public void ThrowsExceptionOnUnexpectedSymbolOptionByDefault()
	{
		var unexpectedOption = "-?";
		var app              = new CommandLineApplication();

		app.Command("test", c => { c.OnExecute(() => 0); });

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("test", unexpectedOption));
		Assert.Equal($"Unrecognized option '{unexpectedOption}'", exception.Message);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedSymbolOption()
	{
		var unexpectedOption = "-?";
		var app              = new CommandLineApplication();

		var testCmd = app.Command("test", c => { c.OnExecute(() => 0); },
			throwOnUnexpectedArg: false);

		// (does not throw)
		app.Execute("test", unexpectedOption);
		var arg = Assert.Single(testCmd.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
	}

	[Fact]
	public void ThrowsExceptionOnUnexpectedOptionBeforeValidSubcommandByDefault()
	{
		var                    unexpectedOption = "--unexpected";
		CommandLineApplication subCmd           = null;
		var                    app              = new CommandLineApplication();

		app.Command("k", c => {
			subCmd = c.Command("run", _ => { });
			c.OnExecute(() => 0);
		});

		var exception = Assert.Throws<CommandParsingException>(() => app.Execute("k", unexpectedOption, "run"));
		Assert.Equal($"Unrecognized option '{unexpectedOption}'", exception.Message);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedOptionBeforeSubcommand()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication();

		CommandLineApplication subCmd = null;
		var testCmd = app.Command("k", c => {
				subCmd = c.Command("run", _ => { });
				c.OnExecute(() => 0);
			},
			throwOnUnexpectedArg: false);

		// (does not throw)
		app.Execute("k", unexpectedOption, "run");

		Assert.Empty(app.RemainingArguments);
		Assert.Equal(new[] { unexpectedOption, "run" }, testCmd.RemainingArguments.ToArray());
		Assert.Empty(subCmd.RemainingArguments);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedOptionAfterSubcommand()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication();

		CommandLineApplication subCmd = null;
		var testCmd = app.Command("k", c => {
			subCmd = c.Command("run", _ => { }, throwOnUnexpectedArg: false);
			c.OnExecute(() => 0);
		});

		// (does not throw)
		app.Execute("k", "run", unexpectedOption);

		Assert.Empty(app.RemainingArguments);
		Assert.Empty(testCmd.RemainingArguments);
		var arg = Assert.Single(subCmd.RemainingArguments);
		Assert.Equal(unexpectedOption, arg);
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedOptionBeforeValidCommand_Default()
	{
		var arguments  = new[] { "--unexpected", "run" };
		var app        = new CommandLineApplication(throwOnUnexpectedArg: false);
		var commandRan = false;
		app.Command("run", c => c.OnExecute(() => {
			commandRan = true;
			return 0;
		}));
		app.OnExecute(() => 0);

		app.Execute(arguments);

		Assert.False(commandRan);
		Assert.Equal(arguments, app.RemainingArguments.ToArray());
	}

	[Fact]
	public void AllowNoThrowBehaviorOnUnexpectedOptionBeforeValidCommand_Continue()
	{
		var unexpectedOption = "--unexpected";
		var app              = new CommandLineApplication(throwOnUnexpectedArg: false, continueAfterUnexpectedArg: true);
		var commandRan       = false;
		app.Command("run", c => c.OnExecute(() => {
			commandRan = true;
			return 0;
		}));
		app.OnExecute(() => 0);

		app.Execute(unexpectedOption, "run");

		Assert.True(commandRan);
		var remaining = Assert.Single(app.RemainingArguments);
		Assert.Equal(unexpectedOption, remaining);
	}
}