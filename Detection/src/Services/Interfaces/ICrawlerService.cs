// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services;

/// <summary>
/// Provides the APIs for query <see cref="Crawler"/>.
/// </summary>
public interface ICrawlerService
{
    /// <summary>
    /// Determine that the request client is crawler.
    /// </summary>
    public bool IsCrawler { get; }

    /// <summary>
    /// Gets the <see cref="Crawler"/> name of the request clients.
    /// </summary>
    public Crawler Name { get; }

    /// <summary>
    /// Gets the <see cref="Version"/> of the request client.
    /// </summary>
    public Version Version { get; }
}