// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public class CustomNullException : ArgumentNullException
{
    public CustomNullException() 
        : base() { }
    public CustomNullException(string message) 
        : base(message) { }
    public CustomNullException(string message, Exception innerException) 
        : base(message, innerException) { }
    

    public static CustomNullException CreateInstance()
        => new CustomNullException();
}