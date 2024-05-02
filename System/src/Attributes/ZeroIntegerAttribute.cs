// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Parameter)]
public sealed class ZeroIntegerAttribute(string message) : Attribute
{
	public string Message { get; init; } = message;
	public bool   IsError { get; init; }

	public ZeroIntegerAttribute()
		: this("The value must be zero integer.") { }
	public ZeroIntegerAttribute(string message, bool error)
		: this(message) => IsError = error;
}
