// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

namespace Wangkanai.Extensions.CommandLine;

public sealed class CommandArgumentEnumerator : IEnumerator<CommandArgument>
{
    private readonly IEnumerator<CommandArgument> _enumerator;

    public CommandArgumentEnumerator(IEnumerator<CommandArgument> enumerator)
        => _enumerator = enumerator;

    object IEnumerator.Current => Current;

    public CommandArgument Current   => _enumerator.Current;
    public void            Dispose() => _enumerator.Dispose();
    public void            Reset()   => _enumerator.Reset();

    public bool MoveNext() => Current is { MultipleValues: true } ||
                              _enumerator.MoveNext();
}