using System;

namespace Wangkanai.Detection
{
    public static class DetectionVersionExtensions
    {
        public static Version ToVersion(this string version)
        {
            version = version.RemoveWhitespace();

            return Version.TryParse(version, out var parsedVersion) ?
                parsedVersion :
                new Version(0, 0);
        }

        /// <summary>
        /// if request is going via google proxy then version will be appended with Firefox/11.0 (via ggpht.com GoogleImageProxy)
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        private static string RemoveWhitespace(this string version) =>            
            version.Contains(" ") ? version.Remove(' ') : version;
    }
}
