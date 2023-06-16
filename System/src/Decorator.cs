// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai;

public class Decorator<TService>
{
	public TService Instance { get; set; }

	public Decorator(TService instance)
		=> Instance = instance;
}

public class Decorator<TService, TImplementation> : Decorator<TService>
	where TImplementation : class, TService
{
	public Decorator(TImplementation instance) : base(instance) { }
}

public class DisposableDecorator<TService> : Decorator<TService>, IDisposable
{
	public DisposableDecorator(TService instance) : base(instance) { }

	public void Dispose()
		=> (Instance as IDisposable)?.Dispose();
}