// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class Feature
    {
        public bool Frames { get; set; }
        public bool Iframes { get; set; }
        public bool Cookie { get; set; }
        public bool Javascript { get; set; }
        public bool Vbscript { get; set; }
        public bool Javaapplets { get; set; }
        public bool ActiveX { get; set; }
    }
}