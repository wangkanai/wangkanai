// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Options;

public sealed class UniversalOption
{
    public string         Account             { get; set; }
    public string         Property            { get; set; }
    public string         Name                { get; set; }
    public bool           DisplayFeatures     { get; set; }
    public bool           ForceSsl            { get; set; }
    public bool           AnonymizeIp         { get; set; }
    public int            SampleRate          { get; set; }
    public int            SiteSpeedSampleRate { get; set; }
    public bool           AlwaysSendReferrer  { get; set; }
    public bool           AllowAnchor         { get; set; }
    public CookieOption   Cookie              { get; }
    public EnhancedOption Enhanced            { get; }
}