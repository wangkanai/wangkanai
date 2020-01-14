// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class BrowserNotFoundException : ArgumentException
    {
        private readonly string? _invalidBrowserName; // unrecognized browser name
        public virtual string? InvalidBrowserName => _invalidBrowserName;
        private static string? DefaultMessage => "Browser Not Supported";

        public BrowserNotFoundException()
            : base(DefaultMessage) { }

        public BrowserNotFoundException(string message)
            : base(message) { }

        public BrowserNotFoundException(string paramName, string message)
            : base(message, paramName) { }

        public BrowserNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public BrowserNotFoundException(string message, string invalidBrowserName, Exception innerException)
            : base(message, innerException)
        {
            _invalidBrowserName = invalidBrowserName;
        }

        public BrowserNotFoundException(string paramName, string invalidBrowserName, string message)
            : base(message, paramName)
        {
            _invalidBrowserName = invalidBrowserName;
        }

        public override string Message
        {
            get
            {
                var s = base.Message;
                if (_invalidBrowserName != null)
                    return s + Environment.NewLine + InvalidBrowserName;

                return s;
            }
        }
    }
}
