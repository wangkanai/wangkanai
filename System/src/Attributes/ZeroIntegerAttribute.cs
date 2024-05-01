// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Parameter)]
public sealed class ZeroIntegerAttribute : Attribute
{
	public string Message { get; init; }
	public bool   IsError { get; init; }
	public ZeroIntegerAttribute() => Message = "The value must be zero integer.";
	public ZeroIntegerAttribute(string message) => Message = message;
	public ZeroIntegerAttribute(string message, bool error) : this(message) => IsError = error;
}
