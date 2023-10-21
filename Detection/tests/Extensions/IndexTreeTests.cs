// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Extensions;

public class IndexTreeTests
{
	[Fact]
	public void Keywords_IsNull()
	{
		string[] _null = null!;
		var      abc   = "abc";
		var      tree  = new IndexTree(_null);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}

	[Fact]
	public void Keywords_IsEmpty()
	{
		var _empty = Array.Empty<string>();
		var abc    = "abc";
		var tree   = new IndexTree(_empty);
		Assert.True(tree.ContainsWithAnyIn(abc));
		Assert.True(tree.StartsWithAnyIn(abc));
	}
}