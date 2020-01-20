// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;

namespace Wangkanai.Detection
{
    //[ComVisible(true)]
    public class BrowserNotFoundException : ArgumentException
    {
        public virtual string InvalidBrowserName { get; }
        public BrowserNotFoundException(string paramName, string message) : base(message, paramName) { }

        public override string Message => InvalidBrowserName != null ? Message + Environment.NewLine + InvalidBrowserName : Message;
    }
}
