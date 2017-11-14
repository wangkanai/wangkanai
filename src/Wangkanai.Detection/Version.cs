// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Version
    {
        public Version(string major, string minor)
        {
            if (major == null) throw new ArgumentNullException(nameof(major));
            if (minor == null) throw new ArgumentNullException(nameof(minor));

            Major = major;
            Minor = minor;
        }
        public Version(string major, string minor, string patch, string build)
            :this(major, minor)
        {
            if (patch == null) throw new ArgumentNullException(nameof(patch));
            if (build == null) throw new ArgumentNullException(nameof(build));

            Patch = patch;
            Build = build;
        }

        public string Major { get;  }
        public string Minor { get;  }
        public string Patch { get;  }
        public string Build { get;  }
    }
}