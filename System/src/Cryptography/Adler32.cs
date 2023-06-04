// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;

namespace Wangkanai.Cryptography;

public class Adler32
{
	const int Base = 65521;
	const int Max  = 5552;

	public static int ComputeChecksum(int initial, byte[] data, int start, int length)
	{
		data.ThrowIfNull();
		initial.ThrowIfLessThan(0);
		start.ThrowIfLessThan(0);
		length.ThrowIfLessThan(0);

		var a = initial         & 0xFFFF;
		var b = (initial >> 16) & 0xFFFF;

		var index = start;
		var end   = start + length;

		while (end > 0)
		{
			var k = end < Max ? end : Max;
			end -= k;
			for (int i = 0; i < k; i++)
			{
				a += data[index++];
				b += a;
			}

			a %= Base;
			b %= Base;
		}

		return b << 16 | a;
	}

	public static int ComputeChecksum(int initial, byte[] data)
		=> ComputeChecksum(initial, data.ThrowIfNull(), 0, data.Length);

	public static int ComputeChecksum(string path)
	{
		path.ThrowIfNull();
		using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);

		return ComputeChecksum(stream);
	}

	public static int ComputeChecksum(Stream stream)
	{
		stream.ThrowIfNull();

		var buffer   = new byte[8172];
		var checksum = 1;
		int size;
		while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
			checksum = ComputeChecksum(checksum, buffer, 0, size);

		return checksum;
	}
}