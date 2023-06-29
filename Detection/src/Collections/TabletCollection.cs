// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
		"kfauwi"
	};

	public static readonly IndexTree KeywordsSearchTree = Keywords.BuildIndexTree();
}