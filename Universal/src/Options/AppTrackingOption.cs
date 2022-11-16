// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Options;

internal sealed class AppTrackingOption : ScreenTrackingOption
{
    public string AppName        { get; set; }
    public string AppId          { get; set; }
    public string AppVersion     { get; set; }
    public string AppInstallerId { get; set; }
}