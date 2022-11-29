// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;

namespace Wangkanai.Cryptography;

public class Adler32
{
    const int Base = 65521;
    const int Max  = 5552;

    public static int ComputeChecksum(int initial, byte[] data, int start, int length)
    {
        Check.NotNull(data);

        var a = initial & 0xFFFF;
        var b = (initial >> 16) & 0xFFFF;

        var index = start;
        var end   = start + length;

        int k;
        while (end > 0)
        {
            k   =  end < Max ? end : Max;
            end -= k;
            for (int i = 0; i < k; i++)
            {
                a += data[index++];
                b += a;
            }

            a %= Base;
            b %= Base;
        }

        return (b << 16) | a;
    }
    
    public static int ComputeChecksum(int initial, byte[] data)
        => ComputeChecksum(initial, data, 0, data.Length);

    public static int ComputeChecksum(Stream stream)
    {
        Check.NotNull(stream);

        byte[] buffer = new byte[8172];
        int    size;
        int    checksum = 1;
        while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
            checksum = ComputeChecksum(checksum, buffer, 0, size);

        return checksum;
    }

    public static int ComputeChecksum(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        return ComputeChecksum(stream);
    }
}