// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Models
{
    public class BrowserFeature
    {
        public bool Frames { get; set; }
        public bool Iframes { get; set; }
        public bool Cookie { get; set; }
        public bool JavaScript { get; set; }
        public bool VbScript { get; set; }
        public bool JavaApplets { get; set; }
        public bool ActiveX { get; set; }
    }
}
