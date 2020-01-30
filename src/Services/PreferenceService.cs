using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly HttpContext _context;

        public Device Preferred
        {
            get => _context.GetPreference();
            private set => _context.SetPreference(value);
        }

        public bool IsSet
        {
            get => _context.GetMark();
            private set => _context.SetMark(value);
        }
        
        public PreferenceService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }

        public void Set(Device preferred)
        {
            IsSet     = true;
            Preferred = preferred;
        }

        public void Clear()
        {
            IsSet     = false;
            Preferred = Device.Desktop;
        }
    }
}