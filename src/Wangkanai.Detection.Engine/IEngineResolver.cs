// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Detection
{
    public class EngineResolver : IEngineResolver
    {
        public IEngine Engine { get; }
        public IUserAgent UserAgent { get; }
    }
}
