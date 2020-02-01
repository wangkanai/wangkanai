namespace Wangkanai.Detection.Services
{
    // public class PreferenceService : IPreferenceService
    // {
    //     private readonly HttpContext _context;
    //     private const    string      PreferenceContextKey = "Preference";
    //
    //     public Device Preferred => Get().Device;
    //
    //     public bool IsSet => Get().Configured;
    //
    //     public PreferenceService(IHttpContextAccessor accessor)
    //     {
    //         _context = accessor.HttpContext;
    //         Set(Device.Desktop, false);
    //     }
    //
    //     public void Set(Device preferred)
    //         => Set(preferred, true);
    //
    //     private void Set(Device preferred, bool configured)
    //     {
    //         var preference = new Preference {Configured = configured, Device = preferred};
    //         _context.Session.Set(PreferenceContextKey, Encoding.ASCII.GetBytes(preference.Serialized()));
    //     }
    //     
    //     private Preference Get()
    //     {
    //         _context.Session.TryGetValue(PreferenceContextKey, out var raw);
    //         var json = Encoding.ASCII.GetString(raw);
    //         return Preference.Deserialized(json);
    //     }
    //
    //     public void Clear()
    //     {
    //         _context.Session.Remove(PreferenceContextKey);
    //     }
    // }

    // public class Preference
    // {
    //     public Device Device { get; set; }
    //
    //     public bool Configured { get; set; }
    //
    //     public string Serialized()
    //         => JsonSerializer.Serialize(this);
    //
    //     public static Preference Deserialized(string json)
    //         => JsonSerializer.Deserialize<Preference>(json);
    // }
}