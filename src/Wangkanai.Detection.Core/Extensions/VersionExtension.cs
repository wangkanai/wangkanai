using System;
using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Detection.Extensions
{
    public static class VersionExtension
    {
        public static Version ToVersion(this string version)
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
