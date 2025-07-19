// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Mvc.Infrastructure;

internal sealed class OrderedEndpointsSequenceProvider
{
	private int _current;

	public int GetNext()
		=> Interlocked.Increment(ref _current);
}
