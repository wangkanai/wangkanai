// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Primitives;

/// <summary>Represents a concrete domain error.</summary>
/// <param name="code">The error code.</param>
/// <param name="message">The error message.</param>
public sealed class Error(string code, string message) : ValueObject
{
	/// <summary>Gets the error code.</summary>
	public string Code { get; } = code;

	/// <summary>Gets the error message.</summary>
	public string Message { get; } = message;

	/// <summary>Implicitly converts an <see cref="Error"/> to a string.</summary>
	/// <param name="error"></param>
	public static implicit operator string(Error error)
		=> error?.Code ?? string.Empty;

	/// <summary>Gets the empty error instance.</summary>
	internal static Error None => new(string.Empty, string.Empty);
}
