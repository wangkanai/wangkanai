// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace System
{
    public class SetDeviceArgumentNullException : ArgumentNullException
    {
        public SetDeviceArgumentNullException(string paramName) : base(paramName) { }
    }
}
