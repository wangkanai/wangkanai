// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Wangkanai.Browser.Depreciated
{
    internal class OperaMiniBrowser : DeviceBrowser
    {
        public override bool IsValid(HttpRequest request)
        {
            // opera mini special case
            if (!request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini")))) return false;

            DeviceInfoDepreciated = DeviceBuilderDepreciated.Mobile();
            return true;
        }
    }
}