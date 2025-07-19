// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentNullOrEmptyException : ArgumentException
{
	private ArgumentNullOrEmptyException(SerializationInfo info, StreamingContext context) { }
	public ArgumentNullOrEmptyException() : base(SystemResources.ArgumentNullOrEmptyGeneric) { }
	public ArgumentNullOrEmptyException(string paramName) : base(paramName, SystemResources.ArgumentNullOrEmptyGeneric) { }
	public ArgumentNullOrEmptyException(string paramName, string message) : base(paramName, message) { }
	public ArgumentNullOrEmptyException(string message, Exception innerException) : base(message, innerException) { }
}
