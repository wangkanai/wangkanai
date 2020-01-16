// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection
{
    public class Client
    {
        public Device Device { get; set; }
        public Browser Browser { get; set; }

        //public Platform Platform { get; set; }
        public Engine Engine { get; set; }

        public string Agent { get; set; }
    }
}
