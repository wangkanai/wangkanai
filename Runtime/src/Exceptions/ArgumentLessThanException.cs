// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

[Serializable]
public class ArgumentLessThanException : ArgumentException
{
    public ArgumentLessThanException()
        : base(SystemResources.ArgumentLessThan_Generic)
    {
    }

    public ArgumentLessThanException(string paramName)
        : base(SystemResources.ArgumentLessThan_Generic, paramName)
    {
    }

    public ArgumentLessThanException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public ArgumentLessThanException(string paramName, string message)
        : base(message, paramName)
    {
    }
}