// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Text.RegularExpressions;
using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PlatformService : IPlatformService
    {
        public Processor Processor { get; }
        public Platform  Name      { get; }
        public Version   Version   { get; }

        public PlatformService(IUserAgentService userAgentService)
        {
            var agent = userAgentService.UserAgent;

            var match = Regex.Match(agent.ToString(), @"\(([^\)]+)\)");
            var sysInfo = match.Success ? match.Groups[1].Value : String.Empty;

            Name      = GetPlatform(sysInfo);
            Processor = GetProcessor(sysInfo, Name);
            Version   = GetVersion(sysInfo, Name);
        }

        private static Platform GetPlatform(string sysInfo)
        {
            // Unknown
            if (sysInfo.IsNullOrEmpty())
                return Platform.Unknown;
            // Google Android
            if (sysInfo.HasAny(Platform.Android))
                return Platform.Android;
            // Microsoft Windows
            if (sysInfo.HasAny(Platform.Windows))
                return Platform.Windows;
            // Apple iOS
            if (sysInfo.HasAny(Platform.iOS, "iPad", "iPhone", "iPod"))
                return Platform.iOS;
            // Apple Mac
            if (sysInfo.HasAny(Platform.Mac))
                return Platform.Mac;
            // Linux Distribution
            if (sysInfo.HasAny(Platform.Linux))
                return Platform.Linux;

            return Platform.Others;
        }

        private static Version GetVersion(string sysInfo, Platform platform) 
            => platform switch
        {
            Platform.Unknown => new Version(),
            Platform.Others  => new Version(),
            Platform.Windows => ParseOsVersion(sysInfo, "windowsnt"),
            Platform.Android => ParseOsVersion(sysInfo, "android"),
            Platform.Mac     => ParseOsVersion(sysInfo, "intelmacosx"),
            Platform.iOS     => ParseOsVersion(sysInfo, "cpuiphoneos"),
            Platform.Linux   => ParseOsVersion(sysInfo, "rv:"),
            _                => new Version()
        };

        private static Version ParseOsVersion(string sysInfo, string versionPrefix) 
        {
            var osPart = sysInfo
                .RemoveAll(" ", "(", ")")
                .Replace("_", ".")
                .Split(';')
                .FirstOrDefault(x => x.StartsWith(versionPrefix, StringComparison.InvariantCultureIgnoreCase)) ?? String.Empty;
            var match = Regex.Match(osPart, @"(?:(\d+)\.)?(?:(\d+)\.)?(?:(\d+)\.\d+)");
            
            return (match.Success ? match.Value: String.Empty).ToVersion();
        }
                

        private static Processor GetProcessor(string sysInfo, Platform os)
        {
            if (IsArm(sysInfo, os))
                return Processor.ARM;
            if (IsX64(sysInfo))
                return Processor.x64;
            if (IsX86(sysInfo))
                return Processor.x86;
            if (IsPowerPC(sysInfo, os))
                return Processor.x64;

            return Processor.Others;
        }

        private static bool IsArm(string sysInfo, Platform os)
            => sysInfo.HasAny(Processor.ARM, Platform.Android)
               || os == Platform.iOS;

        private static bool IsPowerPC(string sysInfo, Platform os)
            => os == Platform.Mac
               && !sysInfo.HasAny("PPC");

        private static bool IsX86(string sysInfo)
            => sysInfo.HasAny(Processor.x86, "i86", "i686");

        private static bool IsX64(string sysInfo)
            => sysInfo.HasAny(Processor.x64.ToString(), "x86_64", "wow64");
    }
}