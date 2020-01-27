// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface ICrawlerService
    {
        bool    IsCrawler { get; }
        Crawler Type      { get; }
        Version Version   { get; }
    }
}