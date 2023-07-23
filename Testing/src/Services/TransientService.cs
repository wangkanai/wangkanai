// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public class TransientService : ITransientService
{
	public DateTime        Start    { get; set; }
	public DateTime        Stop     { get; set; }
	public TimeSpan        Elapsed  => Stop - Start;
	public ServiceLifetime Lifetime => ServiceLifetime.Transient;
}