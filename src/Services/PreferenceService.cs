using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly HttpContext _context;
        private const    string      PreferenceContextKey = "Preference";

        public Device Preferred => _context.GetPreference();

        public bool IsSet => _context.GetMark();

        public PreferenceService(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }

        public void Set(Device preferred)
        {
            var preference = new Preference {Configured = true, Device = preferred};
            _context.Session.SetString(PreferenceContextKey, preference.Serialized());
        }

        public void Get()
        {
            _context.Session.TryGetValue(PreferenceContextKey, out var json);
            var preference = Preference.Deserialized(json.ToString());
        }

        public void Clear()
        {
            _context.Session.Remove(PreferenceContextKey);
        }
    }

    public class Preference
    {
        public Device Device { get; set; }

        public bool Configured { get; set; }

        public string Serialized()
            => JsonSerializer.Serialize(this);

        public static Preference Deserialized(string json)
            => JsonSerializer.Deserialize<Preference>(json);
    }
}