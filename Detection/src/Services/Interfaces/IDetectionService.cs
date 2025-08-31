// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>Provides the APIs for query client detection services.</summary>
public interface IDetectionService
{
   /// <summary>Get the <see cref="UserAgent"/> of the request client.</summary>
   UserAgent UserAgent { get; }

   /// <summary>Get the <see cref="Device"/> resolved of the request client.</summary>
   IDeviceService Device { get; }

   /// <summary>Get the <see cref="Platform"/> resolved of the request client.</summary>
   IPlatformService Platform { get; }

   /// <summary>Get the <see cref="Engine"/> resolved of the request client.</summary>
   IEngineService Engine { get; }

   /// <summary>Get the <see cref="Browser"/> resolved of the request client.</summary>
   IBrowserService Browser { get; }

   /// <summary>Get the <see cref="Crawler"/> resolved of the request client.</summary>
   ICrawlerService Crawler { get; }
}