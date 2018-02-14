// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class Platform : IPlatform
    {
        public string Name { get; set; }
        public PlatformType Type { get; set; }
        public IVersion Version { get; set; }
        public byte Bits { get; set; }

        public Platform()
        {

        }
    }
}