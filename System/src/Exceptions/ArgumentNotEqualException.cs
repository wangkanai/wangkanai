// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentNotEqualException : ArgumentException
{
	private ArgumentNotEqualException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	public ArgumentNotEqualException() : base(SystemResources.ArgumentNotEqualGeneric) { }
	public ArgumentNotEqualException(string paramName) : base(SystemResources.ArgumentNotEqualGeneric, paramName) { }
	public ArgumentNotEqualException(string message,   Exception innerException) : base(message, innerException) { }
	public ArgumentNotEqualException(string message,   string    paramName, Exception innerException) : base(message, paramName) { }
	public ArgumentNotEqualException(string paramName, string    message) : base(message, paramName) { }
}