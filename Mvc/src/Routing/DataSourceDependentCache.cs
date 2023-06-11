// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Mvc.Routing;

internal sealed class DataSourceDependentCache<T> : IDisposable where T : class
{
	private readonly EndpointDataSource               _dataSource;
	private readonly Func<IReadOnlyList<Endpoint>, T> _initializeCore;
	private readonly Func<T>                          _initializer;
	private readonly Action<object?>                  _initializerWithState;

	private readonly object _lock;

	private bool         _initialized;
	private T?           _value;
	private IDisposable? _disposable;
	private bool         _disposed;
	private object       _syncLock;

	public DataSourceDependentCache(EndpointDataSource dataSource, Func<IReadOnlyList<Endpoint>, T> initialize)
	{
		dataSource.ThrowIfNull();
		initialize.ThrowIfNull();

		_dataSource     = dataSource;
		_initializeCore = initialize;
		_initializer    = Initialize;
	}

	[NotNullIfNotNull(nameof(_value))]
	public T? Value
		=> _value;

	[MemberNotNull(nameof(_value))]
	public T EnsureInitialized()
	{
		_syncLock = _lock;
		return LazyInitializer.EnsureInitialized<T>(ref _value, ref _initialized, ref _syncLock, _initializer);
	}

	private T Initialize()
	{
		lock (_lock)
		{
			var changeToken = _dataSource.GetChangeToken();
			_value = _initializeCore(_dataSource.Endpoints);

			if (_disposed)
				return _value;

			_disposable = changeToken.RegisterChangeCallback(_initializerWithState, null);
			return _value;
		}
	}

	public void Dispose()
	{
		lock (_lock)
		{
			if (!_disposed)
			{
				_disposable?.Dispose();
				_disposable = null;
				_disposed   = true;
			}
		}
	}
}