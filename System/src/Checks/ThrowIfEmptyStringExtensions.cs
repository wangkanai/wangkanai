// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using Wangkanai.Exceptions;
using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class ThrowIfEmptyStringExtensions
{
	// Throw if value string is empty
	public static string ThrowIfEmpty(this string? value)
		=> value.ThrowIfNull().ThrowIfEmpty<ArgumentEmptyException>();

	public static string ThrowIfEmpty(this string? value, string message)
		=> value.ThrowIfNull().ThrowIfEmpty<ArgumentEmptyException>(message);

	public static string ThrowIfEmpty<T>(this string? value)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value))
			   : value!;

	public static string ThrowIfEmpty<T>(this string? value, string message)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message)
			   : value!;

	public static string ThrowIfEmpty<T>(this string? value, string message, [InvokerParameterName] string paramName)
		where T : ArgumentException
		=> value.ThrowIfNull<T>().IsEmpty()
			   ? throw ExceptionActivator.CreateArgumentInstance<T>(paramName, message)
			   : value!;
}
