// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Services;

public interface IResponsiveService
{
    public Device View { get; }
    void PreferSet(Device desktop);
    void PreferClear();
    bool HasPreferred();
}