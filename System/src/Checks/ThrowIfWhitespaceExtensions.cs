// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfWhitespaceExtensions
{
	public static string ThrowIfWhitespace([NotNull] this string? value)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>();

	public static string ThrowIfWhitespace([NotNull] this string? value, string message)
		=> value.ThrowIfWhitespace<ArgumentWhitespaceException>(message);

	public static string ThrowIfWhitespace<T>([NotNull] this string? value)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(nameof(value));

	public static string ThrowIfWhitespace<T>([NotNull] this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfWhitespace<T>(message, nameof(value));

	public static string ThrowIfWhitespace<T>([NotNull] this string? value, string message, string paramName)
		where T : ArgumentException
		=> value!.IsWhiteSpace()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;
}
