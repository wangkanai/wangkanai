// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    public enum Engine
    {
        Unknown    = 0,       // Unknown engine
        WebKit     = 1,       // iOs (Safari, WebViews, Chrome <28)
        Blink      = 1 << 1,  // Google Chrome, Opera v15+
        Gecko      = 1 << 2,  // Firefox, Netscape
        Trident    = 1 << 3,  // IE, Outlook
        EdgeHTML   = 1 << 4,  // Microsoft Edge
        Servo      = 1 << 12, // Mozilla & Samsung
        Others     = 1 << 15  // Others
    }
}
