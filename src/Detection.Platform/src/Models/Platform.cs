// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public class Platform : IPlatform
    {
        public string? Name { get; set; }
        public string? Maker { get; set; }
        public OperatingSystem OS { get; set; }
        public Processor CPU { get; set; }
        public Version? Version { get; set; }

        public Platform() { }
    }
}
