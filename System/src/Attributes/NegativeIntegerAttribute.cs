// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Parameter)]
public sealed class NegativeIntegerAttribute : Attribute
{
	public string Message { get; init; }
	public bool   IsError { get; init; }
	public NegativeIntegerAttribute() => Message = "The value must be negative integer.";
	public NegativeIntegerAttribute(string message) => Message = message;
	public NegativeIntegerAttribute(string message, bool error) : this(message) => IsError = error;
}
