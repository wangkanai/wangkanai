// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum Engine
    {
        Unknown    = 0,       // Unknown engine
        WebKit     = 1,       // iOs (Safari, WebViews, Chrome <28)
        Blink      = 1 << 1,  // Google Chrome, Opera v15+
        Gecko      = 1 << 2,  // Firefox, Netscape
        Trident    = 1 << 3,  // IE, Outlook
        EdgeHTML   = 1 << 4,  // Microsoft Edge
        KHTML      = 1 << 5,  // Konqueror
        Presto     = 1 << 6,  //
        Goanna     = 1 << 7,  // Pale Moon
        NetSurf    = 1 << 8,  // NetSurf
        NetFront   = 1 << 9,  // Access NetFront
        Prince     = 1 << 10, //
        Robin      = 1 << 11, // The Bat!
        Servo      = 1 << 12, // Mozilla & Samsung
        Tkhtml     = 1 << 13, // hv3
        Links2     = 1 << 14, // launched with -g
        Others     = 1 << 15  // Others
    }
}
