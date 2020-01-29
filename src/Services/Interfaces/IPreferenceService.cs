using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IPreferenceService
    {
        public Device Preferred { get; }
        public bool IsSet { get; }
        public void   Set(Device preferred);
        public void   Clear();
    }
}