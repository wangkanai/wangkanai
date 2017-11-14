// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

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
}