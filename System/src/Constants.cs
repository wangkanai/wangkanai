// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

/// <summary>Provides application-wide constants to be used throughout the application.</summary>
internal static class Constants
{
	/// <summary>
	/// Specifies the timeout interval for regular expression operations.
	/// This value is used to prevent excessive time consumption during regex evaluations,
	/// serving as a protection against potential regular expression performance issues such as catastrophic backtracking.
	/// </summary>
	public static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(100);
}
