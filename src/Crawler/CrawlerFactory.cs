// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public class CrawlerFactory : ICrawlerFactory
    {
        public string? Name { get; set; }
        public Crawler Type { get; set; }
        public Version? Version { get; set; }

        public CrawlerFactory()
        {
        }

        public CrawlerFactory(string name)
        {
            Name = name;
            Type = GetType(name);
        }

        public CrawlerFactory(string name, string version) : this(name)
        {
            Version = version.ToVersion();
        }

        private Crawler GetType(string name)
        {
            name = name.ToLower();
            if (name == string.Empty || name == null)
                return Crawler.Others;

            if (name.Contains("google"))
                return Crawler.Google;
            if (name.Contains("bing"))
                return Crawler.Bing;
            if (name.Contains("baidu"))
                return Crawler.Baidu;
            if (name.Contains("yahoo"))
                return Crawler.Yahoo;
            if (name.Contains("facebook") || name.Contains("facebot"))
                return Crawler.Facebook;
            if (name.Contains("twitter"))
                return Crawler.Twitter;
            if (name.Contains("linkedin"))
                return Crawler.LinkedIn;
            if (name.Contains("skype"))
                return Crawler.Skype;

            return Crawler.Others;
        }
    }
}
