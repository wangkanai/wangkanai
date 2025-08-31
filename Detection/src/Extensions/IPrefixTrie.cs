// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection.Extensions;

public interface IPrefixTrie
{
   bool IsEnd { get; }
   bool ContainsWithAnyIn(ReadOnlySpan<char> source);
   bool StartsWithAnyIn(ReadOnlySpan<char>   source);
}