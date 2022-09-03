// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Microsoft.AspNetCore.Components;

namespace Wangkanai.Tabler;

public class BaseTablerComponent : ComponentBase, IBaseTablerComponent, IDisposable
{
    protected bool Disposed { get; private set; }

    public virtual void Dispose()
    {
        Disposed = true;
    }
}