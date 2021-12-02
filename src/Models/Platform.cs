// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection.Models;

[Flags]
public enum Platform
{
    Unknown = 0,
    Windows = 1 << 0, // Microsoft Windows
    Mac     = 1 << 1, // Apple MacOS
    iOS     = 1 << 2, // Apple iOS
    iPadOS  = 1 << 3, // Apple iPadOS
    Linux   = 1 << 4, // Linux Distribution (Red Hat, Mint, Ubuntu)
    Android = 1 << 5, // Google Android
    Others  = 1 << 6  // Others
}