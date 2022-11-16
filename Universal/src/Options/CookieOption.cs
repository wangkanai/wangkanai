// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Options;

public sealed class CookieOption
{
    public string Domain       { get; set; }
    public string Name         { get; set; }
    public int    Expires      { get; set; }
    public string LegacyDomain { get; set; }
}