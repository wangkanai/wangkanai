// Copyright (c) 2014-2024 Sarin Na Wangkanai,All Rights Reserved.Apache License,Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai;

[DebuggerStepThrough]
public static class CheckThrowIfNullExtensions
{
	public static bool    ThrowIfNull(this bool?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static byte    ThrowIfNull(this byte?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static sbyte   ThrowIfNull(this sbyte?   value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static short   ThrowIfNull(this short?   value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static ushort  ThrowIfNull(this ushort?  value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static int     ThrowIfNull(this int?     value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static uint    ThrowIfNull(this uint?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static nint    ThrowIfNull(this nint?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static nuint   ThrowIfNull(this nuint?   value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static long    ThrowIfNull(this long?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static ulong   ThrowIfNull(this ulong?   value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static float   ThrowIfNull(this float?   value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static decimal ThrowIfNull(this decimal? value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static double  ThrowIfNull(this double?  value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static char    ThrowIfNull(this char?    value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static string  ThrowIfNull(this string?  value)                 => ThrowIfNull<ArgumentNullException>(value);
	public static string  ThrowIfNull(this string?  value, string message) => ThrowIfNull<ArgumentNullException>(value, message);

	public static bool ThrowIfNull<T>(this bool? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static sbyte ThrowIfNull<T>(this sbyte? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static byte ThrowIfNull<T>(this byte? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static short ThrowIfNull<T>(this short? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static ushort ThrowIfNull<T>(this ushort? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static int ThrowIfNull<T>(this int? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static uint ThrowIfNull<T>(this uint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static nint ThrowIfNull<T>(this nint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static nuint ThrowIfNull<T>(this nuint? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static long ThrowIfNull<T>(this long? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static ulong ThrowIfNull<T>(this ulong? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static float ThrowIfNull<T>(this float? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static double ThrowIfNull<T>(this double? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static decimal ThrowIfNull<T>(this decimal? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static char ThrowIfNull<T>(this char? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	public static string ThrowIfNull<T>(this string? value)
		where T : ArgumentException
		=> value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value));

	[MemberNotNull]
	public static string ThrowIfNull<T>(this string? value, string message)
		where T : ArgumentException
	{
		return value ?? throw ExceptionActivator.CreateArgumentInstance<T>(nameof(value), message);
	}
}
