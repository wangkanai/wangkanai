// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// Modifications Copyright (c) 2020 Kapok Marketing, Inc.
// The Apache v2. See License.txt in the project root for license information.

using System.Diagnostics;

namespace Wangkanai.Detection.Models
{
    public class UserAgent
    {
        private readonly string _useragent;
        private readonly string _useragentLower;
        private readonly int _useragentLength;

        public int Length => _useragentLength;

        public UserAgent()
        {
            _useragent      = string.Empty;
            _useragentLower = string.Empty;
            _useragentLength = 0;
        }

        public UserAgent(string useragent) : this()
        {
            _useragent      = useragent ?? string.Empty;
            _useragentLower = _useragent.ToLower();
            _useragentLength = _useragent.Length;
        }

        [DebuggerStepThrough]
        public override string ToString()
            => _useragent;

        public string ToLower() 
            => _useragentLower;
    }
}