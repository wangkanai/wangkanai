// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Detection
{
    // concept of extension to HttpRequest
    public static class DeviceRequestExtensions
    {
        public static Device Device(this HttpRequest request)
        {
            return new Device();
        }        
    }
}
