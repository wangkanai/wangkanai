// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.SendGrid.Services;

public interface IRazorViewRenderService
{
    Task<string> ViewToHtmlAsync<T>(string viewName, T model);
    Task<string> ViewToPlainAsync<T>(string viewName, T model);
}