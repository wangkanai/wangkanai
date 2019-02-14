// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

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
    }
}