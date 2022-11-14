// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components;

public readonly struct MarkupString
{
    public string Value { get; }

    public MarkupString(string value)
        => Value = value;

    public static explicit operator MarkupString(string value)
        => new MarkupString(value);

    public override string ToString()
        => Value ?? string.Empty;
}