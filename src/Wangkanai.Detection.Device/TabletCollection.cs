using System;
using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Detection
{
    internal static class TabletCollection
    {
        public static string[] Keywords => new string[] {
            "tablet",
            "ipad",
            "playbook",
            "hp-tablet",
            "kindle",
            "sm-t"
        };

        public static string[] Prefixes => new string[] { };
    }

    internal static class CrawlerCollection
    {
        public static string[] Keywords => new string[] {
            "bot",
            "slurp",
            "spider"
        };
    }
    internal static class MobileCollection
    {
        public static string[] Keywords => new string[] {
            "blackberry",
            "webos",
            "ipod",
            "lge vx",
            "midp",
            "maemo",
            "mmp",
            "mobile",
            "netfront",
            "hiptop",
            "nintendo DS",
            "novarra",
            "openweb",
            "opera mobi",
            "opera mini",
            "palm",
            "psp",
            "phone",
            "smartphone",
            "symbian",
            "up.browser",
            "up.link",
            "wap",
            "windows ce"
        };
        // reference 4 chare from http://www.webcab.de/wapua.htm
        public static string[] Prefixes => new string[] {
            "w3c ",
            "w3c-",
            "acs-",
            "alav",
            "alca",
            "amoi",
            "audi",
            "avan",
            "benq",
            "bird",
            "blac",
            "blaz",
            "brew",
            "cell",
            "cldc",
            "cmd-",
            "dang",
            "doco",
            "eric",
            "hipt",
            "htc_",
            "inno",
            "ipaq",
            "ipod",
            "jigs",
            "kddi",
            "keji",
            "leno",
            "lg-c",
            "lg-d",
            "lg-g",
            "lge-",
            "lg/u",
            "maui",
            "maxo",
            "midp",
            "mits",
            "mmef",
            "mobi",
            "mot-",
            "moto",
            "mwbp",
            "nec-",
            "newt",
            "noki",
            "palm",
            "pana",
            "pant",
            "phil",
            "play",
            "port",
            "prox",
            "qwap",
            "sage",
            "sams",
            "sany",
            "sch-",
            "sec-",
            "send",
            "seri",
            "sgh-",
            "shar",
            "sie-",
            "siem",
            "smal",
            "smar",
            "sony",
            "sph-",
            "symb",
            "t-mo",
            "teli",
            "tim-",
            "tosh",
            "tsm-",
            "upg1",
            "upsi",
            "vk-v",
            "voda",
            "wap-",
            "wapa",
            "wapi",
            "wapp",
            "wapr",
            "webc",
            "winw",
            "winw",
            "xda ",
            "xda-"
        };
    }
}