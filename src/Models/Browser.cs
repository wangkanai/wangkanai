// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum Browser
    {
        Unknown          = 0,
        Chrome           = 1 << 0, // Google Chrome
        InternetExplorer = 1 << 1, // Microsoft Internet Explorer
        Safari           = 1 << 2, // Apple Safari
        Firefox          = 1 << 3, // Firefox
        Edge             = 1 << 4, // Microsoft Edge
        Opera            = 1 << 5, // Opera
        Others           = 1 << 6  // Others
    }
}