// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentPositiveException : ArgumentException
{
	private ArgumentPositiveException(SerializationInfo info, StreamingContext context) { }
	public ArgumentPositiveException() : base(SystemResources.ArgumentPositiveGeneric) { }
	public ArgumentPositiveException(string paramName) : base(SystemResources.ArgumentZeroGeneric, paramName) { }
	public ArgumentPositiveException(string message, Exception innerException) : base(message, innerException) { }
	public ArgumentPositiveException(string paramName, string message) : base(message, paramName) { }
}
