using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IPreferenceService
    {
        public Device Preferred { get; }
        public void   Set(Device preferred);
        public void   Clear();
    }
}