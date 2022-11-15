// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Options;

internal sealed class PageviewOption : FieldOption
{
    public string Page        { get; set; }
    public string Title       { get; set; }
    public string HitCallback { get; set; }
    public bool   AnonymizeIp { get; set; }
}