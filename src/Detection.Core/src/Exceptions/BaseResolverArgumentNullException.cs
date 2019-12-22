// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace System
{
    public class BaseResolverArgumentNullException : ArgumentNullException
    {
        public BaseResolverArgumentNullException(string paramName) : base(paramName)
        {
        }
    }
}
