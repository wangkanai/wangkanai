// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Platform : IPlatform
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public PlatformType Type { get; set; }
        public Version Version { get; set; }
        public byte Bits { get; set; }

        public Platform()
        {

        }
    }
}