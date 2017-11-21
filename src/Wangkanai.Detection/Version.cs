// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Version
    {
        public string Major { get; }
        public string Minor { get; }
        public string Patch { get; }
        public string Build { get; }

        public Version(string major, string minor)
        {
            if (major == null || major == string.Empty)
                throw new ArgumentNullException(nameof(major));
            if (minor == null || minor == string.Empty)
                throw new ArgumentNullException(nameof(minor));

            Major = major;
            Minor = minor;
        }
        public Version(string major, string minor, string patch, string build)
            : this(major, minor)
        {
            if (patch == null || patch == string.Empty)
                throw new ArgumentNullException(nameof(patch));
            if (build == null || build == string.Empty)
                throw new ArgumentNullException(nameof(build));

            Patch = patch;
            Build = build;
        }

        public override string ToString()
        {            
            if (Build is null) return $"{Major}.{Minor}.{Patch}";
            if (Patch is null) return $"{Major}.{Minor}";
            if (Minor is null) return $"{Major}";

            return $"{Major}.{Minor}.{Patch}.{Build}";
        }
    }
}