// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Builder;

namespace Wangkanai.Webserver;

public static class ApplicationMarkerExtensions
{
    public static bool VerifyMarkerIsRegistered<T>(this IApplicationBuilder app)
        where T : class
    {
        var type = typeof(T);
        var name = type.Name;
        if (app.ApplicationServices.GetService(type) is null)
            throw new InvalidOperationException($"{name} is not added to ConfigureServices(...)");
        return true;
    }
    
    //public static bool VerifyMarkerIsRegistered<T>(this IApplicationBuilder app)
}