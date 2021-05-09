// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Diagnostics;

namespace Wangkanai.Detection.Models
{
    public class UserAgent
    {
        private readonly string _original;
        private readonly string _lower;
        private readonly int    _length;

        public UserAgent()
        {
            _original = string.Empty;
            _lower    = string.Empty;
            _length   = 0;
        }

        public UserAgent(string useragent) : this()
        {
            _original = useragent ?? string.Empty;
            _lower    = _original.ToLower();
            _length   = _original.Length;
        }

        [DebuggerStepThrough]
        public override string ToString() => _original;

        [DebuggerStepThrough]
        public string ToLower() => _lower;

        [DebuggerStepThrough]
        public int Length() => _length;
    }
}