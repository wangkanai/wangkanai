// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    public class CookieManager : IDeviceManager
    {
        private readonly ResponsiveOptions _options;
        private const string ResponsiveContextKey = "Responsive";
        private readonly HttpContext _context;

        public CookieManager(HttpContext context, ResponsiveOptions options)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _context = context;
            _options = options;
        }
        public DeviceType Device => _options.Default(Get());
        public DeviceType Get()
        {
            var value = _context.Request.Cookies[ResponsiveContextKey];
            DeviceType result;
            Console.Write(value);
            Enum.TryParse<DeviceType>(value, out result);

            return result;
        }

        public void Set(DeviceType value)
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(60);

            _context.Response.Cookies.Append(ResponsiveContextKey, value.ToString(), option);
        }

        public void Remove()
        {
            _context.Response.Cookies.Delete(ResponsiveContextKey);
        }
    }
}
