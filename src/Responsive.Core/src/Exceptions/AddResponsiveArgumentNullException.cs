// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
