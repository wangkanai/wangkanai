// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentZeroException : ArgumentException
{
	private ArgumentZeroException(SerializationInfo info, StreamingContext context) { }
	public ArgumentZeroException() : base(SystemResources.ArgumentZeroGeneric) { }
	public ArgumentZeroException(string paramName) : base(SystemResources.ArgumentZeroGeneric, paramName) { }
	public ArgumentZeroException(string message, Exception innerException) : base(message, innerException) { }
	public ArgumentZeroException(string paramName, string message) : base(message, paramName) { }
}
