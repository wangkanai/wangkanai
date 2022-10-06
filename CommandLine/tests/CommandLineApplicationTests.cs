// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions.CommandLine;

namespace Wangkanai.CommandLine.Tests;

public class CommandLineApplicationTests
{
    [Fact]
    public void CommandNameCanBeMatched()
    {
        var called = false;

        var app = new CommandLineApplication();
        app.Command("test", c =>
        {
            c.OnExecute(() =>
            {
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
        CommandArgument first  = null;
        CommandArgument second = null;

        var app = new CommandLineApplication();

        app.Command("test", c =>
        {
            first  = c.Argument("first", "First argument");
            second = c.Argument("second", "Second argument");
            c.OnExecute(() => 0);
        });

        app.Execute("test", "one", "two");

        Assert.Equal("one", first.Value);
        Assert.Equal("two", second.Value);
    }

    [Fact]
    public void ExtraArgumentCausesException()
    {
        CommandArgument first  = null;
        CommandArgument second = null;
        
        var app = new CommandLineApplication();

        app.Command("test", c =>
        {
            first = c.Argument("first", "First argument");
            second = c.Argument("second", "Second argument");
            c.OnExecute(()=>0);
        });
        
        var ex = Assert.Throws<CommandParsingException>(()=>app.Execute("test", "one", "two", "three"));
    }
}