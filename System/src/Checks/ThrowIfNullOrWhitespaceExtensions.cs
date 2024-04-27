// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfNullOrWhitespaceExtensions
{
	public static string ThrowIfNullOrWhitespace([NotNull] this string? value)
		=> value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>();

	public static string ThrowIfNullOrWhitespace([NotNull] this string? value, string message)
		=> value.ThrowIfNullOrWhitespace<ArgumentNullOrWhitespaceException>(message);

	public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(nameof(value));

	public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNullOrWhitespace<T>(message, nameof(value));

	public static string ThrowIfNullOrWhitespace<T>([NotNull] this string? value, string message, string paramName)
		where T : ArgumentException
		=> value!.IsNullOrWhiteSpace()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;
}
