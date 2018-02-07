// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface IBrowser
    {
        string Name { get; set; }
        string Maker { get; set; }
        BrowserType Type { get; set; }
        IVersion Version { get; set; }
    }
}