// Copyright (c) 2014-2025 Sarin Na Wangkanai and Aliaksandr Kukrash, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public interface IPrefixTrie
{
	bool ContainsWithAnyIn(ReadOnlySpan<char> source);
	bool StartsWithAnyIn(ReadOnlySpan<char>   source);
	bool IsEnd { get; }
}
