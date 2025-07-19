// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

[DebuggerStepThrough]
public static class StreamExtensions
{
	/// <summary>
	/// Copies the content of one stream to another stream.
	/// </summary>
	/// <param name="fromStream">The stream from which to read the content.</param>
	/// <param name="toStream">The stream to which the content will be copied.</param>
	public static void CopyTo(this Stream fromStream, Stream toStream)
	{
		fromStream.ThrowIfNull();
		toStream.ThrowIfNull();

		var bytes = new byte[16 * 1024];
		int dataRead;
		while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
			toStream.Write(bytes, 0, dataRead);
	}

	/// <summary>
	/// Reads the entire content of a stream and returns it as a byte array.
	/// </summary>
	/// <param name="stream">The stream from which to read the content.</param>
	/// <returns>The content of the stream as a byte array.</returns>
	public static byte[] ReadFully(this Stream stream)
	{
		stream.ThrowIfNull();

		var buffer = new byte[16 * 1024];
		using var ms = new MemoryStream();
		int read;
		while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
			ms.Write(buffer, 0, read);

		return ms.ToArray();
	}

	/// <summary>
	/// Reads the content of a stream and returns it as a string.
	/// </summary>
	/// <param name="stream">The stream from which to read the content.</param>
	/// <returns>The content of the stream as a string.</returns>
	public static string ReadToString(this Stream stream)
	{
		stream.ThrowIfNull();
		// convert stream to string
		using var reader = new StreamReader(stream);
		return reader.ReadToEnd();
	}
}
