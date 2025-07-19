// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Detection.Models;

[Flags]
public enum Engine
{
	Unknown = 1 << 0, // Unknown engine
	WebKit = 1 << 1, // iOs (Safari, WebViews, Chrome <28) (https://webkit.org/)
	Blink = 1 << 2, // Google Chrome, Opera v15+ (https://www.chromium.org/Home)
	Gecko = 1 << 3, // Firefox, Netscape (https://hg.mozilla.org/mozilla-central/)
	Trident = 1 << 4, // IE, Outlook
	Edge = 1 << 5, // Microsoft Edge
	Servo = 1 << 6, // Mozilla & Samsung (https://github.com/servo/servo)
	Others = 1 << 7  // Others
}
