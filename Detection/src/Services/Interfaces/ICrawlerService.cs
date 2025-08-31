// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>Provides the APIs for query <see cref="Crawler"/>.</summary>
public interface ICrawlerService
{
   /// <summary>Determine that the request client is crawler.</summary>
   bool IsCrawler { get; }

   /// <summary>Gets the <see cref="Crawler"/> name of the request clients.</summary>
   Crawler Name { get; }

   /// <summary>Gets the <see cref="Version"/> of the request client.</summary>
   Version Version { get; }
}