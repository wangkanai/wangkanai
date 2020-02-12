// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection.Models
{
    [Flags]
    public enum Platform
    {
        Unknown = 0,
        Windows = 1 << 0, // Microsoft Windows
        Mac     = 1 << 1, // Apple MacOS
        iOS     = 1 << 2, // Apple iOS
        Linux   = 1 << 3, // Linux Distribution (Red Hat, Mint, Ubuntu)
        Android = 1 << 4, // Google Android
        Others  = 1 << 5  // Others
    }
}