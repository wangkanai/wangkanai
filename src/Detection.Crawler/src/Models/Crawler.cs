// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Crawler : ICrawler
    {
        public string Name { get; set; }
        public CrawlerType Type { get; set; }
        public Version Version { get; set; }

        public Crawler()
        {
        }

        public Crawler(string name)
        {
            Name = name;
            Type = GetType(name);
        }

        public Crawler(string name, string version) : this(name)
        {
            Version = version.ToVersion();
        }

        private CrawlerType GetType(string name)
        {
            name = name.ToLower();
            if (name == string.Empty || name == null)
                return CrawlerType.Others;

            if (name.Contains("google"))
                return CrawlerType.Google;
            if (name.Contains("bing"))
                return CrawlerType.Bing;
            if (name.Contains("baidu"))
                return CrawlerType.Baidu;
            if (name.Contains("yahoo"))
                return CrawlerType.Yahoo;
            if (name.Contains("facebook") || name.Contains("facebot"))
                return CrawlerType.Facebook;
            if (name.Contains("twitter"))
                return CrawlerType.Twitter;
            if (name.Contains("linkedin"))
                return CrawlerType.LinkedIn;
            if (name.Contains("skype"))
                return CrawlerType.Skype;

            return CrawlerType.Others;
        }
    }
}
