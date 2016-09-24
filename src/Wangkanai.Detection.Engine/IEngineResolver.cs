using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Detection
{
    public class EngineResolver : IEngineResolver
    {
        public IEngine Engine { get; }
        public IUserAgent UserAgent { get; }
    }
}
