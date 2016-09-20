// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

// ReSharper disable once CheckNamespace

using Wangkanai.Detection;

namespace Microsoft.AspNetCore.Http
{
    // concept of extension to HttpRequest
    // static class has issue with dependency injection
    public static class DeviceRequestExtensions
    {
        public static Device Device(this HttpRequest request)
        {
            return new Device();
        }        
    }
}
