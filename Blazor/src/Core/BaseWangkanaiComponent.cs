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