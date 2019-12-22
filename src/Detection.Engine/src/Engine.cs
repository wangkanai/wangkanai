// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Engine : IEngine
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public EngineType Type { get; set; }
        public Version Version { get; set; }
    }
}
