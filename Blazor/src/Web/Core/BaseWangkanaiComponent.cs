// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor;

public class BaseWangkanaiComponent : ComponentBase, IBaseWangkanaiComponent, IDisposable
{
    protected bool Disposed { get; private set; }

    public virtual void Dispose()
    {
        Disposed = true;
    }
}