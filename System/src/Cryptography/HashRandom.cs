// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Cryptography;

using ArgumentException = System.ArgumentException;

namespace Wangkanai.Cryptography;

public class HashRandom : Random
{
	private static readonly RandomNumberGenerator Generator = RandomNumberGenerator.Create();

	private readonly byte[] _buffer = new byte[4];

	public HashRandom() { }

	public HashRandom(int seedIgnored) { }
	
	public static byte[] CreateRandomKey(int length)
	{
		var bytes = new byte[length];
		Generator.GetBytes(bytes);
		return bytes;
	}
	
	public static string CreateUniqueId(int length = 32, OutputFormat format = OutputFormat.Base64Url)
	{
		var bytes = CreateRandomKey(length);
		return format switch
		{
			OutputFormat.Base64Url   => Base64Url.Encode(bytes),
			OutputFormat.Base64      => Convert.ToBase64String(bytes),
			OutputFormat.Hexadecimal => BitConverter.ToString(bytes).Replace("-", ""),
			_                        => throw new ArgumentException("Invalid output format", nameof(format))
		};
	}

	public enum OutputFormat
	{
		Base64Url,
		Base64,
		Hexadecimal
	}
}