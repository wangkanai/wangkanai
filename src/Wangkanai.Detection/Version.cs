// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class Version
    {
        public Version(string major, string minor)
        {
            Major = major;
            Minor = minor;
        }
        public Version(string major, string minor, string patch, string build)
            :this(major, minor)
        {
            Patch = patch;
            Build = build;
        }

        public string Major { get;  }
        public string Minor { get;  }
        public string Patch { get;  }
        public string Build { get;  }
    }
}