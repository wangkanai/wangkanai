// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Models
{
    public class BrowserFeature
    {
        public bool Frames { get; set; } = false;
        public bool Iframes { get; set; } = false;
        public bool Cookie { get; set; } = false;
        public bool JavaScript { get; set; } = false;
        public bool VbScript { get; set; } = false;
        public bool JavaApplets { get; set; } = false;
        public bool ActiveX { get; set; } = false;
    }
}
