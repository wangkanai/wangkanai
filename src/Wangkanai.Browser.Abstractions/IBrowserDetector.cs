using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wangkanai.Browser.Abstractions
{
    public interface IBrowserDetector
    {
        string Device { get; }
        string Platform { get;}
        string Engine { get; }
    }
}
