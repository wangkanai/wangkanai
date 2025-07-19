// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public interface ILifetimeService
{
	public DateTime Start { get; set; }
	public DateTime Stop { get; set; }
	public TimeSpan Elapsed { get; }
	public ServiceLifetime Lifetime { get; }
}
