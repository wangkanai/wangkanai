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
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Renders the component to the supplied <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" />.
	/// </summary>
	/// <param name="builder">A <see cref="T:Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder" /> that will receive the render output.</param>
	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		throw new NotImplementedException();
	}
}