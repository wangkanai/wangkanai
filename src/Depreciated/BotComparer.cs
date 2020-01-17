// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Wangkanai.Detection
{
    [Obsolete]
    public class BotComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.Contains(y);
        }

        public int GetHashCode(string obj)
        {
            if (obj == null || obj == string.Empty) return 0;

            return obj.GetHashCode();
        }
    }
}
