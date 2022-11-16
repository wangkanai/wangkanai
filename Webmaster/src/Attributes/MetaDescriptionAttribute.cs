// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Webmaster.Core;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class MetaDescriptionAttribute : Attribute
{
    private readonly string _description;

    public MetaDescriptionAttribute(string description)
    {
        _description = description;
    }
}