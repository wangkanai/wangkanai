// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface ICrawler
    {
        string Name { get; set; }
        CrawlerType Type { get; set; }
        IVersion Version { get; set; }
    }
}
