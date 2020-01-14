// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public class EngineFactory : IEngineFactory
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public Engine Type { get; set; }
        public Version Version { get; set; }
    }
}
