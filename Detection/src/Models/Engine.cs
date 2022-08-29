// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Engine
{
    Unknown = 0,      // Unknown engine
    WebKit = 1 << 0, // iOs (Safari, WebViews, Chrome <28) (https://webkit.org/)
    Blink = 1 << 1, // Google Chrome, Opera v15+ (https://www.chromium.org/Home)
    Gecko = 1 << 2, // Firefox, Netscape (https://hg.mozilla.org/mozilla-central/)
    Trident = 1 << 3, // IE, Outlook
    Edge = 1 << 4, // Microsoft Edge
    Servo = 1 << 5, // Mozilla & Samsung (https://github.com/servo/servo)
    Others = 1 << 6  // Others
}