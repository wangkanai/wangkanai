// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Attributes;

// Positive

class IntegerConstructorDefault
{
	public IntegerConstructorDefault() { }
}

class PositiveIntegerConstructorExist
{
	[PositiveInteger]
	public PositiveIntegerConstructorExist() { }
}

class PositiveIntegerConstructorError
{
	[PositiveInteger("error", true)]
	public PositiveIntegerConstructorError() { }
}

// Negative

class NegativeIntegerConstructorExist
{
	[NegativeInteger]
	public NegativeIntegerConstructorExist() { }
}

class NegativeIntegerConstructorError
{
	[NegativeInteger("error", true)]
	public NegativeIntegerConstructorError() { }
}

// Zero

class ZeroIntegerConstructorExist
{
	[ZeroInteger]
	public ZeroIntegerConstructorExist() { }
}

class ZeroIntegerConstructorError
{
	[ZeroInteger("error", true)]
	public ZeroIntegerConstructorError() { }
}
