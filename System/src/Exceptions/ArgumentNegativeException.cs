﻿// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentNegativeException : ArgumentException
{
	private ArgumentNegativeException(SerializationInfo info, StreamingContext context) { }
	public ArgumentNegativeException() : base(SystemResources.ArgumentNegativeGeneric) { }
	public ArgumentNegativeException(string paramName) : base(SystemResources.ArgumentZeroGeneric, paramName) { }
	public ArgumentNegativeException(string message, Exception innerException) : base(message, innerException) { }
	public ArgumentNegativeException(string paramName, string message) : base(message, paramName) { }
}
