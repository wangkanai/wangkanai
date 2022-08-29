// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Universal.Options;

namespace Wangkanai.Universal.Models
{
    public class ScreenTracking : Send
    {
        private ScreenTrackingOption option { get; set; }

        public ScreenTracking(string name)
        {
            option = new ScreenTrackingOption();
            option.screenName = name;
        }

        public override string ToString()
        {
            return "ga('send','screenview'," + option + "});";
        }
    }
}