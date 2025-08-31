// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public interface ILifetimeService
{
   DateTime        Start    { get; set; }
   DateTime        Stop     { get; set; }
   TimeSpan        Elapsed  { get; }
   ServiceLifetime Lifetime { get; }
}