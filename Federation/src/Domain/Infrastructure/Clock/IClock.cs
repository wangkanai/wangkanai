// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation;

public interface IClock
{
	DateTimeOffset UtcNow { get; }
}
