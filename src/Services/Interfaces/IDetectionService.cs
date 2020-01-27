// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IDetectionService
    {
        UserAgent        UserAgent { get; }
        IDeviceService   Device    { get; }
        ICrawlerService  Crawler   { get; }
        IPlatformService Platform  { get; }
        IEngineService   Engine    { get; }
        IBrowserService  Browser   { get; }
    }
}