// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum OperatingSystem
    {
        Windows = 0,      // Microsoft Windows
        Mac     = 1 << 0, // Apple MacOS
        iOS     = 1 << 1, // Apple iOS
        Linux   = 1 << 2, // Linux Distribution (Red Hat, Mint, Ubuntu)
        Android = 1 << 3, // Google Android
        Others  = 1 << 4  // Others
    }
}
