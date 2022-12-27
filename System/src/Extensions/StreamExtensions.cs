// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;

namespace Wangkanai.Extensions;

public static class StreamExtensions
{
    public static void CopyTo(this Stream fromStream, Stream toStream)
    {
        fromStream.ThrowIfNull();
        toStream.ThrowIfNull();

        var bytes = new byte[8092];
        int dataRead;
        while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
            toStream.Write(bytes, 0, dataRead);
    }

    public static byte[] ReadFully(this Stream stream)
    {
        stream.ThrowIfNull();

        byte[]    buffer = new byte[16 * 1024];
        using var ms     = new MemoryStream();
        int       read;
        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            ms.Write(buffer, 0, read);

        return ms.ToArray();
    }

    public static string ReadToString(this Stream stream)
    {
        stream.ThrowIfNull();
        // convert stream to string
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}