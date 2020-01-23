using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services
{
    public interface IUserPreferenceService
    {
        public Device Preferred { get; }
    }
}
