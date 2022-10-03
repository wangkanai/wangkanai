// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

public sealed class CommandOption
{
    public string            Template   { get; set; }
    public CommandOptionType OptionType { get; set; }

    public CommandOption(string template, CommandOptionType optionType)
    {
        Template   = template;
        OptionType = optionType;
    }

    public string       ShortName      { get; set; }
    public string       LongName       { get; set; }
    public string       SymbolName     { get; set; }
    public string       Description    { get; set; }
    public List<string> Values         { get; set; }
    public bool         ShowInHelpText { get; set; }
    public bool         Inherited      { get; set; }

    public bool TryParse(string value)
    {
        switch (OptionType)
        {
            case CommandOptionType.MultipleValue:
                Values.Add(value);
                break;
            case CommandOptionType.SingleValue:
                if (Values.Any())
                    return false;
                Values.Add(value);
                break;
            case CommandOptionType.NoValue:
                if (value != null)
                    return false;
                Values.Add("on");
                break;
            default:
                break;
        }

        return true;
    }

    public bool HasValue()
        => Values.Any();

    public string Value()
        => HasValue() ? Values[0] : null;

    private static bool IsEnglishLetter(char c)
        => (c >= 'a' && c <= 'z') || c >= 'A' && c <= 'Z';
}