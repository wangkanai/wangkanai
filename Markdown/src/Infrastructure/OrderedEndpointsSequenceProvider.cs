// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class OrderedEndpointsSequenceProvider
{
	private int _current;

	public int GetNext()
		=> Interlocked.Increment(ref _current);
}