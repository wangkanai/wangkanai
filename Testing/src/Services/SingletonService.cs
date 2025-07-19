// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public class SingletonService : ISingletonService
{
	public DateTime Start { get; set; }
	public DateTime Stop { get; set; }
	public TimeSpan Elapsed => Stop - Start;
	public ServiceLifetime Lifetime => ServiceLifetime.Singleton;
}
