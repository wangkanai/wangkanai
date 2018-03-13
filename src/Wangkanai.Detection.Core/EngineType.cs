// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public enum EngineType
    {
        WebKit,   // iOs (Safari, WebViews, Chrome <28)
        Blink,    // Google Chrome, Opera v15+
        Gecko,    // Firefox, Netscape
        Trident,  // IE, Outlook
        EdgeHTML, // Microsoft Edge
        KHTML,    // Konqueror
        Presto,   // 
        Goanna,   // Pale Moon
        NetSurf,  // NetSurf
        NetFront, // Access NetFront
        Prince,   // 
        Robin,    // The Bat!
        Servo,    // Mozilla & Samsung
        Tkhtml,   // hv3
        Links2,   // launched with -g
        Others
    }
}
