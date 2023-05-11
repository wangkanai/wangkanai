// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class MarkdownActionEndpointDataSourceIdProvider
{
	private int _nextId = 1;

	internal int CreateId()
		=> Interlocked.Increment(ref _nextId);
}