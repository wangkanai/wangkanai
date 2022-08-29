// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Collections.Generic;

namespace Wangkanai.Blazor;

public class BaseMapper
{
    public List<Func<string>> Items = new List<Func<string>>();
}

public static class BaseMapperExtensions
{
    public static T Add<T>(this T w, string name) where T : BaseMapper
    {
        w.Items.Add(() => name);
        return w;
    }

    public static T Get<T>(this T w, Func<string> funcName) where T : BaseMapper
    {
        w.Items.Add(funcName);
        return w;
    }

    public static T GetIf<T>(this T w, Func<string> funcName, Func<bool> func) where T : BaseMapper
    {
        w.Items.Add(() => func() ? funcName() : null);
        return w;
    }

    public static T If<T>(this T w, string name, Func<bool> func) where T : BaseMapper
    {
        w.Items.Add(() => func() ? name : null);
        return w;
    }
}