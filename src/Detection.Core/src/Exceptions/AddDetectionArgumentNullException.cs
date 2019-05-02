using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class AddDetectionArgumentNullException : ArgumentNullException
    {
        public AddDetectionArgumentNullException(string paramName) : base(paramName) { }
    }
}
