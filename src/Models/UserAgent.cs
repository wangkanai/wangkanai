// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Diagnostics;

namespace Wangkanai.Detection.Models
{
    public class UserAgent
    {
        private readonly string _useragent;
        private readonly string _useragentLower;

        public UserAgent()
        {
            _useragent      = string.Empty;
            _useragentLower = string.Empty;
        }

        public UserAgent(string useragent) : this()
        {
            _useragent      = useragent ?? string.Empty;
            _useragentLower = _useragent.ToLower();
        }

        [DebuggerStepThrough]
        public override string ToString()
            => _useragent;

        public string ToLower() 
            => _useragentLower;
    }
}