// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai;

[DebuggerStepThrough]
public static class CheckThrowIfNullExtensions
{
	// Throw if value is null (all possible types)
	[MemberNotNull]
	public static bool ThrowIfNull(this bool? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static byte ThrowIfNull(this byte? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static sbyte ThrowIfNull(this sbyte? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static short ThrowIfNull(this short? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static ushort ThrowIfNull(this ushort? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static int ThrowIfNull(this int? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static uint ThrowIfNull(this uint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static nint ThrowIfNull(this nint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static nuint ThrowIfNull(this nuint? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static long ThrowIfNull(this long? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static ulong ThrowIfNull(this ulong? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static float ThrowIfNull(this float? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static decimal ThrowIfNull(this decimal? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static double ThrowIfNull(this double? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static char ThrowIfNull(this char? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static string ThrowIfNull(this string? value)
	{
		return ThrowIfNull<ArgumentNullException>(value);
	}

	[MemberNotNull]
	public static string ThrowIfNull(this string? value, string message)
	{
		return ThrowIfNull<ArgumentNullException>(value, message);
	}

	[MemberNotNull]
	public static bool ThrowIfNull<T>(this bool? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static sbyte ThrowIfNull<T>(this sbyte? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static byte ThrowIfNull<T>(this byte? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static short ThrowIfNull<T>(this short? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static ushort ThrowIfNull<T>(this ushort? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static int ThrowIfNull<T>(this int? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static uint ThrowIfNull<T>(this uint? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static nint ThrowIfNull<T>(this nint? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static nuint ThrowIfNull<T>(this nuint? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static long ThrowIfNull<T>(this long? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static ulong ThrowIfNull<T>(this ulong? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static float ThrowIfNull<T>(this float? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static double ThrowIfNull<T>(this double? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static decimal ThrowIfNull<T>(this decimal? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static char ThrowIfNull<T>(this char? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static string ThrowIfNull<T>(this string? value)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));
	}

	[MemberNotNull]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message);
	}
}
