// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Cryptography;

namespace Wangkanai.Cryptography;

public class HashRandom : Random
{
	private static readonly RandomNumberGenerator Generator = RandomNumberGenerator.Create();

	private readonly byte[] _buffer = new byte[4];

	public HashRandom() { }

	public HashRandom(int seedIgnored) { }

	public static byte[] CreateRandomKey(int length)
	{
		length.ThrowIfLessThan(1);

		var bytes = new byte[length];
		Generator.GetBytes(bytes);
		return bytes;
	}

	public static string CreateUniqueId(int length = 32, OutputFormat format = OutputFormat.Base64Url)
	{
		length.ThrowIfLessThan(1);

		var bytes = CreateRandomKey(length);
		return format switch
		{
			OutputFormat.Base64Url => Base64Url.Encode(bytes),
			OutputFormat.Base64    => Convert.ToBase64String(bytes),
			OutputFormat.Hex       => BitConverter.ToString(bytes).Replace("-", ""),
			_                      => throw new ArgumentOutOfRangeException("Invalid output format", nameof(format))
		};
	}

	public override int Next()
	{
		Generator.GetBytes(_buffer);
		return BitConverter.ToInt32(_buffer, 0) & 0x7FFFFFFF;
	}

	public override int Next(int maxValue)
	{
		maxValue.ThrowIfLessThan(0);
		return Next(0, maxValue);
	}

	public override int Next(int minValue, int maxValue)
	{
		minValue.ThrowIfMoreThan(maxValue);
		if (minValue == maxValue) return minValue;
		var diff = maxValue - minValue;

		while (true)
		{
			Generator.GetBytes(_buffer);
			var random = BitConverter.ToUInt32(_buffer, 0);
			var max    = 1 + (long)uint.MaxValue;
			var remainder = max % diff;
			if (random < max - remainder)
				return (int)(minValue + (random % diff));
		}
	}

	public override double NextDouble()
	{
		Generator.GetBytes(_buffer);
		var random = BitConverter.ToUInt32(_buffer, 0);
		return random / (1.0 + uint.MaxValue);
	}

	public override void NextBytes(byte[] buffer)
	{
		buffer.ThrowIfNull();
		Generator.GetBytes(buffer);
	}

	public enum OutputFormat
	{
		Base64Url,
		Base64,
		Hex
	}
}