using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IPreferenceService
    {
        Device Preferred { get; }
        bool   IsSet     { get; }
        void   Set(Device preferred);
        void   Clear();
    }
}