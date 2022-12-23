// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0
//


namespace Wangkanai.Tabler;

public abstract class TablerComponentBase : BlazorComponentBase, ITablerComponentBase, IDisposable
{
    protected new bool Disposed { get; private set; }

    public new virtual void Dispose()
    {
        Disposed = true;
    }
}