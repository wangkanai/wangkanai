// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Cryptography;

namespace Wangkanai.Cryptography;

public class Adler32 : HashAlgorithm
{
	public override void Initialize()
	{
		throw new NotImplementedException();
	}

	protected override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		throw new NotImplementedException();
	}

	protected override byte[] HashFinal()
	{
		throw new NotImplementedException();
	}
}