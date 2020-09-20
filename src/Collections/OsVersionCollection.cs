using System;
using System.Collections.Generic;

namespace Wangkanai.Detection.Collections
{
    public static class OsVersionCollection
    {
        private static Dictionary<string, Version> _macOsVersions = new Dictionary<string, Version>()
        {
            {"Intel Mac OS X 10.5", new Version("10.5")},
            {"Intel Mac OS X 10.6", new Version("10.6")},
            {"Intel Mac OS X 10.7", new Version("10.7")},
            {"Intel Mac OS X 10.8", new Version("10.8")},
            {"Intel Mac OS X 10.9", new Version("10.9")},
            {"Intel Mac OS X 10_10", new Version("10.10")},
            {"Intel Mac OS X 10_10_0", new Version("10.10")},
            {"Intel Mac OS X 10_10_1", new Version("10.10.1")},
            {"Intel Mac OS X 10_10_2", new Version("10.10.2")},
            {"Intel Mac OS X 10_10_3", new Version("10.10.3")},
            {"Intel Mac OS X 10_10_4", new Version("10.10.4")},
            {"Intel Mac OS X 10_10_5", new Version("10.10.5")},
            {"Intel Mac OS X 10_11", new Version("10.11")},
            {"Intel Mac OS X 10_11_0", new Version("10.11.0")},
            {"Intel Mac OS X 10_12", new Version("10.12")},
        };

        public static Dictionary<string, Version> Versions => _macOsVersions;


    }
}
