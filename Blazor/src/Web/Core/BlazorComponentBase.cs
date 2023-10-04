// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Wangkanai.Blazor;

public abstract class BlazorComponentBase : ComponentBase, IBlazorComponentBase, IDisposable
{
	protected bool Disposed { get; private set; }

	public virtual void Dispose()
	{
		Disposed = true;
	}

	protected virtual void BuildRenderTree(RenderTreeBuilder builder) { }
}