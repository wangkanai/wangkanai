// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Security.Cryptography;
using System.Text;

namespace Wangkanai.Cryptography;

public sealed class Adler32 : HashAlgorithm
{
	// Reference: https://en.wikipedia.org/wiki/Adler-32

	private const ushort Base = 0xFFF1; // 65521
	private const ushort Max  = 0x15B0; //  5552

	private ushort _a;
	private ushort _b; 
	
	public Adler32()
		=> Initialize();

	public static int Checksum(string text)
		=> Checksum(Encoding.ASCII.GetBytes(text));

	public static int Checksum(byte[] bytes)
	{
		bytes.ThrowIfNull();
		
		var adler32 = new Adler32();
		adler32.HashCore(bytes, 0, bytes.Length);
		return BitConverter.ToInt32(adler32.HashFinal(), 0);
	}

	public override int HashSize => 32;

	public override void Initialize()
	{
		// reset the sum values
		_a = 1; // 0x0001
		_b = 0; // 0x0000
	}

	protected override void HashCore(byte[] data, int start, int length)
	{
		var index = start;
		var end   = start + length;

		while (end > 0)
		{
			var endian = end < Max ? end : Max;
			end -= endian;
			for (int i = 0; i < endian; i++)
			{
				_a += data[index++];
				_b += _a;
			}

			_a %= Base;
			_b %= Base;
		}
	}

	protected override byte[] HashFinal()
	{
		var concat = (uint)(_b << 16) | _a;
		return BitConverter.GetBytes(concat);
	}
}