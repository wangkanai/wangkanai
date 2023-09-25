// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Resources;

namespace Wangkanai.Exceptions;

[Serializable]
public sealed class ArgumentLessThanException : ArgumentException
{        
	private ArgumentLessThanException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	public ArgumentLessThanException() : base(SystemResources.ArgumentLessThanGeneric) { }
	public ArgumentLessThanException(string paramName) : base(SystemResources.ArgumentLessThanGeneric, paramName) { }
	public ArgumentLessThanException(string message,   Exception innerException) : base(message, innerException) { }
	public ArgumentLessThanException(string paramName, string    message) : base(message, paramName) { }
}