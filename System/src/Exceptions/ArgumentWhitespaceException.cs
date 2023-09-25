// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentWhitespaceException : ArgumentException
{
	public ArgumentWhitespaceException() : base(SystemResources.ArgumentEmptyGeneric) { }
	public ArgumentWhitespaceException(string paramName) : base(SystemResources.ArgumentEmptyGeneric, paramName) { }
	public ArgumentWhitespaceException(string message,   Exception innerException) : base(message, innerException) { }
	public ArgumentWhitespaceException(string paramName, string    message) : base(message, paramName) { }
}