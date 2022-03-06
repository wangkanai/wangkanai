// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[Serializable]
public class ArgumentNullOrEmptyException : ArgumentNullException
{
    public ArgumentNullOrEmptyException()
    {
    }

    public ArgumentNullOrEmptyException(string paramName)
        : base(paramName)
    {
    }

    public ArgumentNullOrEmptyException(string paramName, string message)
        : base(paramName, message)
    {
    }

    public ArgumentNullOrEmptyException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}