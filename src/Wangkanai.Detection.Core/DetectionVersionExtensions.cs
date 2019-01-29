using System;

namespace Wangkanai.Detection
{
    public static class DetectionVersionExtensions
    {
        public static Version ToVersion(this string version)
        {
            version = ReplaceWhitespace(version);

            return Version.TryParse(version, out var parsedVersion) ?
                parsedVersion :
                new Version(0, 0);
        }

        private static string ReplaceWhitespace(string version)
        {
            // if request is going via google proxy then version will be appended with Firefox/11.0 (via ggpht.com GoogleImageProxy)            
            return version.Contains(" ") ?
                version.Substring(0, version.IndexOf(' ')) :
                version;
        }
    }
}
