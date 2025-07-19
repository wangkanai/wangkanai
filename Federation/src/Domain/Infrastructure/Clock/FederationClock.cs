// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation;

public sealed class FederationClock : IClock
{
	public DateTimeOffset UtcNow => _timeProvider.GetUtcNow();

	private readonly TimeProvider _timeProvider;

	public FederationClock()
		=> _timeProvider = TimeProvider.System;

	public FederationClock(TimeProvider timeProvider)
		=> _timeProvider = timeProvider;
}
