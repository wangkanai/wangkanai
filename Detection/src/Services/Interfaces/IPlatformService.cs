// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>Provides the APIs for query client platform.</summary>
public interface IPlatformService
{
   /// <summary>Gets the <see cref="Processor"/> of the request client.</summary>
   Processor Processor { get; }

   /// <summary>Gets the <see cref="Platform"/> of the request client.</summary>
   Platform Name { get; }

   /// <summary>Gets the <see cref="Version"/> of the request client.</summary>
   Version Version { get; }
}