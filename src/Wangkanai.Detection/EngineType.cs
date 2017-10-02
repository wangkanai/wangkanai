using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
