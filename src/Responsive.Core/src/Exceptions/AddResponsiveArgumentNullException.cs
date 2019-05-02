using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public class AddResponsiveArgumentNullException : ArgumentNullException
    {
        public AddResponsiveArgumentNullException(string paramName) : base(paramName) { }
    }
}
