// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public class BrowserResolver : IBrowserResolver
    {
        public IBrowser Browser { get; }
        public IUserAgent UserAgent { get; }
        public Version Version { get; set; }
    }
}