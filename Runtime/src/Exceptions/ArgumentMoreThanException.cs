// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[Serializable]
public class ArgumentMoreThanException : ArgumentException
{
    public ArgumentMoreThanException()
        : base(SystemResources.ArgumentMoreThan_Generic)
    {
    }

    public ArgumentMoreThanException(string paramName)
        : base(SystemResources.ArgumentMoreThan_Generic, paramName)
    {
    }

    public ArgumentMoreThanException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ArgumentMoreThanException(string paramName, string message)
        : base(message, paramName)
    {
    }
}