using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Browser
{
    public interface IBrowserService
    {
        UserAgent UserAgent { get; }
        Device Device { get; }
        // waiting to implement in the future
        //Platform Platform{get;}
        //Engine Engine{get;}
    }
}
