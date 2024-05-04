// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

/// <summary>
/// Specifies that a data field value must be a negative integer.
/// </summary>
/// <param name="message"></param>
[AttributeUsage( AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class NegativeIntegerAttribute(string message) : Attribute
{
	public string Message { get; init; } = message;
	public bool   IsError { get; init; }

	public NegativeIntegerAttribute()
		: this("The value must be negative integer.") { }
	public NegativeIntegerAttribute(string message, bool error)
		: this(message) => IsError = error;
}
