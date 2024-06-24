// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class StreamExtensionsTests
{
	[Fact]
	public void CopyTo()
	{
		// Arrange
		var fromStream = new MemoryStream();
		var toStream   = new MemoryStream();
		var bytes      = new byte[8092];
		fromStream.Write(bytes, 0, bytes.Length);
		fromStream.Position = 0;

		// Act
		fromStream.CopyTo(toStream);

		// Assert
		Assert.Equal(fromStream.Length, toStream.Length);
	}

	[Fact]
	public void ReadFully()
	{
		// Arrange
		var stream = new MemoryStream();
		var bytes  = new byte[8092];
		stream.Write(bytes, 0, bytes.Length);
		stream.Position = 0;

		// Act
		var result = stream.ReadFully();

		// Assert
		Assert.Equal(stream.Length, result.Length);
	}

	[Fact]
	public void ReadToString()
	{
		// Arrange
		var length = 8092;
		var stream = new MemoryStream();
		var bytes  = new byte[length];
		stream.Write(bytes, 0, length);
		stream.Position = 0;

		// Act
		var result = stream.ReadToString();

		// Assert
		Assert.Equal(length, result.Length);
	}

	[Fact]
	public void ReadToString_Null()
	{
		// Arrange
		Stream? stream = null;

		// Assert
		Assert.Throws<ArgumentNullException>(() => stream!.ReadToString());
	}

	[Fact]
	public void ReadFully_Null()
	{
		// Arrange
		Stream stream = null!;

		// Assert
		Assert.Throws<ArgumentNullException>(() => stream.ReadFully());
	}

	[Fact]
	public void CopyTo_Null()
	{
		// Arrange
		Stream fromStream = null!;
		var    toStream   = new MemoryStream();

		// Assert
		Assert.Throws<NullReferenceException>(() => fromStream.CopyTo(toStream));
	}
}
