// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Linq.Expressions;
using System.Reflection;

using Xunit.Abstractions;

namespace Wangkanai.Validation.Models;

public class BaseModel
{
    public static PropertyInfo GetProperty<T>(string name)
        where T : BaseModel
        => typeof(T).GetProperty(name);
}

public class BaseModel<T>
{
    public static PropertyInfo GetProperty(string name)
        => typeof(T).GetProperty(name);
}