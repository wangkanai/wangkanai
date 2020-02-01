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

        public void PreferSet(Device desktop)
            => _context.Session.SetString(ResponsiveContextKey, desktop.ToString());

        public void PreferClear()
            => _context.Session.Remove(ResponsiveContextKey);

        public bool IsPreferred()
            => _context.SafeSession().Keys.Any(k => k == ResponsiveContextKey);

        private Device PreferView()
        {
            if (!IsPreferred())
                return Device.Unknown;
            var session = _context.SafeSession();
            if (session.Keys.All(k => k != ResponsiveContextKey))
                return Device.Unknown;

            _context.Session.TryGetValue(ResponsiveContextKey, out var raw);
            Enum.TryParse<Device>(Encoding.ASCII.GetString(raw), out var preferred);
            return preferred;
        }

        private static Device DefaultView(Device device, ResponsiveOptions options)
            => device switch
            {
                Device.Mobile  => options.DefaultMobile,
                Device.Tablet  => options.DefaultTablet,
                Device.Desktop => options.DefaultDesktop,
                _              => device
            };
    }
}