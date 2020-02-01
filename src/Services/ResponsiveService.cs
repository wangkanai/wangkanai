// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class ResponsiveService : IResponsiveService
    {
        public Device View { get; }

        private readonly HttpContext _context;
        private const    string      ResponsiveContextKey = "Responsive";

        public ResponsiveService(IHttpContextAccessor accessor, IDeviceService deviceService, DetectionOptions options)
        {
            if (accessor == null)
                throw new ArgumentNullException(nameof(accessor));
            if (deviceService == null)
                throw new ArgumentNullException(nameof(deviceService));
            if (options == null)
                options = new DetectionOptions();

            _context = accessor.HttpContext;

            View = DefaultView(deviceService.Type, options.Responsive);
            var preferView = PreferView();
            if (preferView != Device.Unknown && preferView != View)
                View = PreferView();
        }

        private static Device DefaultView(Device device, ResponsiveOptions options)
            => device switch
            {
                Device.Mobile  => options.DefaultMobile,
                Device.Tablet  => options.DefaultTablet,
                Device.Desktop => options.DefaultDesktop,
                _              => device
            };

        private Device PreferView()
        {
            var session = _context.SafeSession();
            if (session == null)
                return Device.Unknown;
            if (session.Keys.All(k => k != ResponsiveContextKey))
                return Device.Unknown;

            _context.Session.TryGetValue(ResponsiveContextKey, out var raw);
            var preferred = Encoding.ASCII.GetString(raw);
            Enum.TryParse<Device>(preferred, out var result);
            return result;
        }
    }
}