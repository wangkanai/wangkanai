// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection;

// ReSharper disable once CheckNamespace
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
