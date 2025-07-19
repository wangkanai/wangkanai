// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public interface ILifetimeService
{
	public DateTime Start { get; set; }
	public DateTime Stop { get; set; }
	public TimeSpan Elapsed { get; }
	public ServiceLifetime Lifetime { get; }
}
