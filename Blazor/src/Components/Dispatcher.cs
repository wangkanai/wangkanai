// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Blazor.Components.Sections;

namespace Wangkanai.Blazor.Components;

public abstract class Dispatcher
{
	private SectionRegistry? _sectionRegistry;

	internal SectionRegistry Registry => _sectionRegistry ??= new SectionRegistry();

	public static Dispatcher CreateDefault()
	{
		return new RendererSynchronizationContextDispatcher();
	}

	internal event UnhandledExceptionEventHandler? UnhandledException;

	public abstract bool          CheckAccess();
	public abstract Task          InvokeAsync(Action                       workItem);
	public abstract Task          InvokeAsync(Func<Task>                   workItem);
	public abstract Task<TResult> InvokeAsync<TResult>(Func<TResult>       workItem);
	public abstract Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem);

	public void AssertAccess()
	{
		if (!CheckAccess())
		{
			throw new InvalidOperationException(
				"The current thread is not associated with the Dispatcher. " +
				"Use InvokeAsync() to switch execution to the Dispatcher when " +
				"triggering rendering or component state.");
		}
	}

	protected void OnUnhandledException(UnhandledExceptionEventArgs e)
	{
		if (e is null)
			throw new ArgumentNullException(nameof(e));

		UnhandledException?.Invoke(this, e);
	}
}