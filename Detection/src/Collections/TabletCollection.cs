// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Extensions;

namespace Wangkanai.Detection.Collections;

internal static class TabletCollection
{
	private static readonly string[] Keywords =
	{
		"tablet",
		"ipad",
		"playbook",
		"hp-tablet",
		"kindle",
		"sm-t",
		"kfauwi",
		"mediapad",
		"matepad"
	};

	public static readonly IPrefixTrie KeywordsSearchTrie = Keywords.BuildSearchTrie();
}
