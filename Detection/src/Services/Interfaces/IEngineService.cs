// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>Provides the APIs for query client browser rendering engine.</summary>
public interface IEngineService
{
   /// <summary>Get the <see cref="Engine"/> of the request client.</summary>
   Engine Name { get; }
}