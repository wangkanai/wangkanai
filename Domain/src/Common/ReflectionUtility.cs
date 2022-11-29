// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;

namespace Wangkanai.Domain.Common;

public static class ReflectionUtility
{
    public static bool IsAssignableFromGenericList(this Type type)
    {
        foreach (var intType in type.GetInterfaces())
        {
            if (intType.IsGenericType
                && intType.GetGenericTypeDefinition() == typeof(IList<>))
                return true;
        }

        return false;
    }
}