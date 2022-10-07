// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

namespace Wangkanai.Extensions.CommandLine;

internal sealed class CommandArgumentEnumerator : IEnumerator<CommandArgument>
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

// private class CommandArgumentEnumerator : IEnumerator<CommandArgument>
// {
//     private readonly IEnumerator<CommandArgument> _enumerator;
//
//     public CommandArgumentEnumerator(IEnumerator<CommandArgument> enumerator)
//     {
//         _enumerator = enumerator;
//     }
//
//     public CommandArgument Current
//     {
//         get
//         {
//             return _enumerator.Current;
//         }
//     }
//
//     object IEnumerator.Current
//     {
//         get
//         {
//             return Current;
//         }
//     }
//
//     public void Dispose()
//     {
//         _enumerator.Dispose();
//     }
//
//     public bool MoveNext()
//     {
//         if (Current == null || !Current.MultipleValues)
//         {
//             return _enumerator.MoveNext();
//         }
//
//         // If current argument allows multiple values, we don't move forward and
//         // all later values will be added to current CommandArgument.Values
//         return true;
//     }
//
//     public void Reset()
//     {
//         _enumerator.Reset();
//     }
// }