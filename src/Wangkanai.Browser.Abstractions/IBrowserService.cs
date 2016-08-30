using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Browser.Abstractions
{
    public interface IBrowserService
    {
        string UserAgent();
        string Device();
        // waiting to implement in the future
        //string Platform();
        //string Engine();
    }
}
