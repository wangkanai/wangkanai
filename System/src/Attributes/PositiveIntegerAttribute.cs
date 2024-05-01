// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Parameter)]
public sealed class PositiveIntegerAttribute : Attribute
{
	public string Message { get; init; }
	public bool   IsError { get; init; }
	public PositiveIntegerAttribute() => Message = "The value must be positive integer.";
	public PositiveIntegerAttribute(string message) => Message = message;
	public PositiveIntegerAttribute(string message, bool error) : this(message) => IsError = error;
}
