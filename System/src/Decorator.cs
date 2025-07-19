// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai;

/// <summary>Represents a generic decorator that wraps an instance of a specified type.</summary>
/// <param name="instance">The instance to be wrapped and implemented.</param>
/// <typeparam name="TService">The type of the instance to be wrapped.</typeparam>
public class Decorator<TService>(TService instance)
{
	public TService Instance { get; } = instance;
}

/// <summary>Represents a generic decorator that wraps an instance of a specified type.</summary>
/// <param name="instance">The instance to be wrapped and implemented.</param>
/// <typeparam name="TService">The type of the instance to be wrapped.</typeparam>
/// <typeparam name="TImplementation">The type of the instance to be implemented.</typeparam>
public class Decorator<TService, TImplementation>(TImplementation instance) : Decorator<TService>(instance)
	where TImplementation : class, TService;

/// <summary>Represents a generic decorator that wraps an instance of a specified type and implements <see cref="IDisposable"/>.</summary>
/// <param name="instance">The instance to be wrapped and implemented.</param>
/// <typeparam name="TService">The type of the instance to be wrapped.</typeparam>
public class DisposableDecorator<TService>(TService instance) : Decorator<TService>(instance), IDisposable
{
	/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
	public void Dispose()
	{
		(Instance as IDisposable)?.Dispose();
		GC.SuppressFinalize(this);
	}
}
