// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface IDetection
    {
        IBrowser Browser { get; }
        ICrawler Crawler { get; }
        IDevice Device { get; }
        IEngine Engine { get; }
        IPlatform Platform { get; }
        IUserAgent UserAgent { get; }
    }
}
