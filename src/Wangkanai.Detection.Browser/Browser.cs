// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Reflection.Emit;

namespace Wangkanai.Detection
{
    public class Browser : IBrowser
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public BrowserType Type { get; set; } = BrowserType.Generic;
        public Version Version { get; set; }

        public Browser() { }
        public Browser(BrowserType browserType)
            => Type = browserType;
        public Browser(BrowserType browserType, Version version)
            : this(browserType)
            => Version = version;

        public Browser(string name)
        {
            BrowserType type;

            if (!Enum.TryParse(name, true, out type))
                throw new BrowserNotFoundException(name, "not found");

            Type = type;
        }

        protected Version ParseVersion(string version)
        {
            // if request is going via google proxy then version will be appended with Firefox/11.0 (via ggpht.com GoogleImageProxy)
            if (version.Contains(" "))
            {
                version = version.Substring(0, version.IndexOf(' '));
            }

            if (Version.TryParse(version, out var parsedVersion))
            {
                return parsedVersion;
            }
           return new Version(0, 0);
        }
    }
}