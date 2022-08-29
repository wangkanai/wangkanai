// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Detection.Models;

[Flags]
public enum Crawler
{
    Unknown = 0,
    Google = 1 << 0,
    Bing = 1 << 1,
    Yahoo = 1 << 2,
    Baidu = 1 << 3,
    Facebook = 1 << 4,
    Twitter = 1 << 5,
    LinkedIn = 1 << 6,
    WhatsApp = 1 << 7,
    Skype = 1 << 8,
    Others = 1 << 9
}