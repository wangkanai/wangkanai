// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Attributes;

public class IntegerParameter
{
	public void Default() { }

	public void PositiveExist([PositiveInteger]                int value) { }
	public void PositiveError([PositiveInteger("error", true)] int value) { }
	public void ZeroExit([ZeroInteger]                         int value) { }
	public void ZeroError([ZeroInteger("error", true)]         int value) { }
	public void NegativeExist([NegativeInteger]                int value) { }
	public void NegativeError([NegativeInteger("error", true)] int value) { }
}
