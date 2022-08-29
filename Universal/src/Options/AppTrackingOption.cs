// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Options
{
    internal class AppTrackingOption : ScreenTrackingOption
    {
        public string appName { get; set; }
        public string appId { get; set; }
        public string appVersion { get; set; }
        public string appInstallerId { get; set; }
    }
}