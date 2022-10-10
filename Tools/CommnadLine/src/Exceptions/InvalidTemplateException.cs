// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine;

public sealed class InvalidTemplateException: ArgumentException
{
    public InvalidTemplateException(string template)
        : base($"Invalid template pattern '{template}'", nameof(template))
    {
    }
    
}