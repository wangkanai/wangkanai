// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class BrowserNotFoundException : ArgumentException
    {
        private static string DefaultMessage => "Browser Not Supported";

        public BrowserNotFoundException() : base(DefaultMessage) { }
        public BrowserNotFoundException(string message) : base(message) { }
        public BrowserNotFoundException(string paramName, string message) : base(message, paramName) { }
    }
}