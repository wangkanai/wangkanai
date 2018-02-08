// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Linq;

namespace Wangkanai.Detection
{
    public class Version : IVersion
    {
        public string Major { get; }
        public string Minor { get; }
        public string Patch { get; }
        public string Build { get; }

        public Version(string version)
        {
            if (version == null || version == string.Empty)
                throw new ArgumentNullException(nameof(version));

            var split = version.Split('.').ToList();

            if (split.Count > 0) Major = split[0];
            if (split.Count > 1) Minor = split[1];
            if (split.Count > 2) Patch = split[2];
            if (split.Count > 3) Build = split[3];
        }

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
            if (Major != null && Minor != null && Patch != null && Build != null)
                return $"{Major}.{Minor}.{Patch}.{Build}";
            if (Major != null && Minor != null && Patch != null)
                return $"{Major}.{Minor}.{Patch}";
            if (Major != null && Minor != null)
                return $"{Major}.{Minor}";
            if (Major != null)
                return $"{Major}";

            return "No version resolved";
        }
    }
}