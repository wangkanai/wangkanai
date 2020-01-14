// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace System
{
    //public class ResponsiveMiddlewareArgumentNullException : ArgumentNullException
    //{
    //    public ResponsiveMiddlewareArgumentNullException(string paramName) : base(paramName)
    //    {
    //    }
    //}

    public class ResponsiveMiddlewareNextArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareNextArgumentNullException(string paramName) : base(paramName)
        {
        }
    }

    public class ResponsiveMiddlewareOptionArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareOptionArgumentNullException(string paramName) : base(paramName)
        {
        }
    }

    public class ResponsiveMiddlewareInvokeArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareInvokeArgumentNullException(string paramName) : base(paramName)
        {
        }
    }
}
