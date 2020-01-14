// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Microsoft.AspNetCore.Http;

using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    //public class CookieManager : IDeviceManager
    //{
    //    private readonly ResponsiveOptions _options;
    //    private const string ResponsiveContextKey = "Responsive";
    //    private readonly HttpContext _context;

    //    public CookieManager(
    //        HttpContext context,
    //        ResponsiveOptions options)
    //    {
    //        _context = context
    //            ?? throw new CookieManagerArgumentNullException(nameof(context));
    //        _options = options
    //            ?? throw new CookieManagerArgumentNullException(nameof(options));
    //    }

    //    public DeviceType Device
    //        => _options.Default(Get());

    //    public DeviceType Get()
    //    {
    //        var value = _context.Request.Cookies[ResponsiveContextKey];

    //        Enum.TryParse<DeviceType>(value, out var result);

    //        return result;
    //    }

    //    public void Set(DeviceType value)
    //    {
    //        var option = new CookieOptions
    //        {
    //            Expires = DateTime.Now.AddMinutes(60)
    //        };

    //        _context.Response.Cookies.Append(ResponsiveContextKey, value.ToString(), option);
    //    }

    //    public void Remove()
    //    {
    //        _context.Response.Cookies.Delete(ResponsiveContextKey);
    //    }
    //}
}
