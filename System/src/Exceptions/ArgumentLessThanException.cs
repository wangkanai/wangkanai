// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.System.Resources;

namespace Wangkanai.System.Exceptions;

[Serializable]
public class ArgumentLessThanException : ArgumentException
{
    public ArgumentLessThanException() : base(SystemResources.ArgumentLessThanGeneric) { }
    public ArgumentLessThanException(string paramName) : base(SystemResources.ArgumentLessThanGeneric, paramName) { }
    public ArgumentLessThanException(string message,   Exception innerException) : base(message, innerException) { }
    public ArgumentLessThanException(string paramName, string    message) : base(message, paramName) { }
}