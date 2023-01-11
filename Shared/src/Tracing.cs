// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics;

namespace Wangkanai.Internal;

internal static class Tracing
{
	private static readonly Version AssemblyVersion = typeof(Tracing).Assembly.GetName().Version;

	/// <summary>
	/// Service version
	/// </summary>
	public static string ServiceVersion => AssemblyVersion.ToString(3);

	public static ActivitySource BasicActivitySource { get; } = new(TraceNames.Basic, ServiceVersion);
	public static ActivitySource StoreActivitySource { get; } = new(TraceNames.Store, ServiceVersion);

	public static class TraceNames
	{
		public static string Basic      => "Wangkanai";
		public static string Store      => Basic + ".Store";
		public static string Cache      => Basic + ".Cache";
		public static string Services   => Basic + ".Services";
		public static string Validation => Basic + ".Validation";
	}
}