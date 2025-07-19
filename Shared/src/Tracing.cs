// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;

namespace Wangkanai.Internal;

internal static class Tracing
{
	private static readonly Version AssemblyVersion = typeof(Tracing).Assembly.GetName().Version!;

	/// <summary>
	/// Service version
	/// </summary>
	public static string ServiceVersion => AssemblyVersion.ToString(3);

	/// <summary>
	/// Service name for base tracing
	/// </summary>
	public static ActivitySource BasicActivitySource { get; } = new(TraceNames.Basic, ServiceVersion);

	/// <summary>
	/// Service name for store tracing
	/// </summary>
	public static ActivitySource StoreActivitySource { get; } = new(TraceNames.Store, ServiceVersion);

	/// <summary>
	/// Service name for cache tracing
	/// </summary>
	public static ActivitySource CacheActivitySource { get; } = new(TraceNames.Cache, ServiceVersion);

	/// <summary>
	/// Service name for service tracing
	/// </summary>
	public static ActivitySource ServiceActivitySource { get; } = new(TraceNames.Service, ServiceVersion);

	/// <summary>
	/// Service name for validation tracing
	/// </summary>
	public static ActivitySource ValidationActivitySource { get; } = new(TraceNames.Validation, ServiceVersion);

	public static class TraceNames
	{
		public static string Basic => "Wangkanai";
		public static string Store => Basic + ".Store";
		public static string Cache => Basic + ".Cache";
		public static string Service => Basic + ".Service";
		public static string Validation => Basic + ".Validation";
	}
}
